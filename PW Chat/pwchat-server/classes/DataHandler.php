<?php
require_once("DatabaseHandler.php");
require_once("EncryptionHandler.php");

class DataHandler{
	//create a salt to send with mesages
//helps protect encryption key
//32 is equal to one block (8 bit chars * 32 = 256)
//48 is used since it is guaranteed to split between at least 2 blocks
//this function is also identical to http://shrtrl.tk/keygen so you can use this
//as well for generating an authkey
function saltgen($amount = 48){
	$saltset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
	for ($i=0; $i<$amount; $i++)
		$randsalt .= substr($saltset, rand(0,strlen($saltset)-1), 1);
	return $randsalt;
}
//iv parameter works just like in client, generate a new iv if true
//use the one from keyfile if false
function formjson($data, $iv = true){
	$payload = null;
	$sha512 = null;
	$hash = base64_encode(hash("sha512", $data, true));
	global $encryption;
	//$encryption is determined by request
	//if request sends encrypted data return encrypted
	//if ENCRYPTION is true send encrytped regardless of client
	if(ENCRYPTION || $encryption){
		$e = null;
		$d = null;
		$key = readkeyfile();
		if($iv){
			$iv = base64_encode(mcrypt_create_iv(32));
			$d[0] = 1;
			$d[1] = $hash;
			$d[2] = encrypt($data, $key[0], $iv);
			$d[3] = $iv;
		} else {
			$d[0] = 1;
			$d[1] = $hash;
			$d[2] = encrypt($data, $key[0], $key[1]);
		}
		$payload = json_encode($d);
	} else {
		$d[0] = 0;
		$d[1] = $hash;
		$d[2] = base64_encode($data); //if its in plaintext it kersplodes
		$payload = json_encode($d);
	}
	return $payload;
}
function readjson($data){
	$data = json_decode($data, true);
	$rdata = null;
	$key = readkeyfile();
	if($data[0]){
		if(count($data) > 3){
			//iv is in json
			$rdata = decrypt($data[2], $key[0], $data[3]);
		} else {
			//use keyfile iv
			$rdata = decrypt($data[2], $key[0], $key[1]);
		}
	} else {
		$rdata = base64_decode($data[2]);
	}
	$hash = base64_encode(hash("sha512", $rdata, true));
	if($hash == $data[1]){
		return json_decode($rdata, true);
	}
	return false;
}
//WARNING!!!! This may be fast(only takes 2 seconds on my laptop to parse 100,000 lines)
//but uses insane amounts of RAM, make sure max memory in php.ini is set
//to at least 512mb (100,000 lines uses ~300 mb)
//I'll try to work on memory usage a bit but until then memory usage will be
//very high for initial log parse
//starts at $start
function logparse($logloc = null, $start = 0){
	//$start--;//subtract one since this is 0 indexed //disregard that
	//using ?: only works in php 5.3+ 
	$logloc = $logloc ?: LOGLOC; //use $logloc = $logloc ? $logloc : LOGLOC on lower versions
	$file = file($logloc);
	$i = 0;
	foreach($file as $f){
		if($i >= $start){
			$ofile[] = explode(" ", $f);
		}
		$i++;
		/*
		$ofile[0] = date
		$ofile[1] = time
		$ofile[2] = hostname
		$ofile[3] = glinkd-id
		$ofile[4] = chat (the word chat)
		$ofile[5] = : (yes a colon)
		$ofile[6] = Chat type
		$ofile[7] = src=id
		$ofile[8] = chl=id OR (if whisper) dst=
		$ofile[9] = msg=
		*/
	}
	$i = 0;
	foreach($ofile as $o){
	//clean up the array a bit
		$out[] = array('date' => $o[0]." ".$o[1],
						'type' => str_replace(":", "", $o[6]),
						'uid' => substr($o[7], 4),
						'chldst' => substr($o[8], 4),
						'msg' => base64_encode(iconv("UCS-2LE", "UTF-8", base64_decode(substr($o[9], 4)))));
		//delete array entry after it moves it into the cleaned up array
		//it does seem to save a small amount of memory but hey... it something
		unset($ofile[$i]);
	}
	return array('filelen' => count($file), 'data' => $out);
}
function initdb(){
	$db = mysqli_connect(SQLHOST, SQLUSER, SQLPASS, SQLDB);
	if($db){
		$q = mysqli_query($db, SQLTABLE);
		if($q){
			return true;
		}
	}
	return false;
}
function log2db(){
	$db = mysqli_connect(SQLHOST, SQLUSER, SQLPASS, SQLDB);
	if($db){
		$qar = logparse(LOGLOC, loglen());
		$parts = array();
		$i = 0;
		$n = 0;
		foreach($qar['data'] as $q){
			$i++;
			$uid = $q['uid'];
			$type = $q['type'];
			$chldst = $q['chldst'];
			$time = $q['date'];
			$msg = $q['msg'];
			$parts[$n][] = "('$uid', '$type', '$chldst', '$time', '$msg')";
			//I've tried experimenting a bit... 1000 seems like a good number to use
			if(!($i % 1000)){
				$n++;
			}
		}
		foreach($parts as $p){
			$query  = "INSERT INTO `messages` (`uid`, `type`, `chldst`, `time`, `message`)";
			$query .= "VALUES ".implode(', ', $p);
			if(!mysqli_query($db, $query)){
				return false;
			}
		}
		loglen($qar['filelen']);
		return true;
	}
	return false;
}
//use this instead of row count so everything can be in mysql
function loglen($newlen = -1, $llen = "len.len"){
	$f = fopen($llen, "r");
	if($f){
		if($newlen == -1){
			$flen = (int)fgets($f);
			return $flen;
		}
		$f = fopen($llen, "w");
		fwrite($f, $newlen);
		return true;
	}
	return false;
}

//credits to gouranga for hexdump and broadcast functions
function hexdump($data, $htmloutput = true, $uppercase = false, $return = false){
		$hexi   = '';
		$ascii  = '';
		$dump   = ($htmloutput === true) ? '<pre>' : '';
		$offset = 0;
		$len    = strlen($data);
		$x = ($uppercase === false) ? 'x' : 'X';
		for ($i = $j = 0; $i < $len; $i++){
				$hexi .= sprintf("%02$x ", ord($data[$i]));     // Convert to hexidecimal
				if (ord($data[$i]) >= 32)                       // Replace non-viewable bytes with '.'
						$ascii .= ($htmloutput === true) ? htmlentities($data[$i]) : $data[$i];
				else
						$ascii .= '.';
				if (++$j === 16 || $i === $len - 1){
						$dump .= sprintf("%04$x  %-49s  %s", $offset, $hexi, $ascii); // Join the hexi / ascii output
						$hexi = $ascii = '';
						$offset += 16;
						$j = 0;
						if ($i !== $len - 1)
								$dump .= "\n";
				}
		}
		$dump .= $htmloutput === true ? '</pre>' : '';
		$dump .= "\n";
		if ($return === false) {
			echo $dump;
		} else {
			return $dump;
		}
}
function broadcast($message){
	$sock = socket_create(AF_INET, SOCK_STREAM, SOL_TCP);
	if($sock){
		if(socket_connect($sock, DELIVERYDH, DELIVERYDP)){
			socket_set_block($sock);
			$data2 = mByte(9) . mByte(0) . mInt(-1) . mInt(0) . mString($message);
			$data = mUInt32(79) . mUInt32(strlen($data2)) . $data2;
			socket_send($sock, $data, 8192, 0);
			socket_set_nonblock($sock);
			socket_close($sock);
			return true;
		} else {
			return false;
		}
	}
}
function getrolename($id){
	//figure out how far off role ID is from account ID
	$offset = $id % 8;
	$sock = socket_create(AF_INET, SOCK_STREAM, SOL_TCP);
	if($sock){
		if(socket_connect($sock, GAMEDBDH, GAMEDBDP)){
			socket_set_block($sock);
			$data = mUInt32(3032) . mByte(8) . mShort(32768) . mShort(0x0025) . mInt($id-$offset);
			//hexdump($data);
			socket_send($sock, $data, 8192, 0);
			socket_recv($sock, $buf, 8192, 0);
			//hexdump($buf);
			socket_set_nonblock($sock);
			socket_close($sock);
			//for some reason the number of names
			//doesn't always stay in the same spot
			$pos11 = ord(substr($buf, 11, 1));
			$pos12 = ord(substr($buf, 12, 1));
			if($pos11 == 0){
				$namelen = $pos12;
				$pholder = 17;
			} else {
				$namelen = $pos11;
				$pholder = 16;
			}
			
			for($i = 0; $i < $namelen; $i++){
				$thisnamelen = ord(substr($buf, $pholder, 1));
				$names[] = iconv("UCS-2LE", "UTF-8", substr($buf, $pholder+1, $thisnamelen));
				//+1 is because it needs to advance one to get where the name starts
				//+$thisnamelen pushes it all the way to end of name
				//+4 then skips over the 'useless' bytes to get next name length
				$pholder += 1 + $thisnamelen + 4;
			}
			if($names[$offset] != ""){
				return $names[$offset];
			}
		}
	}
	return "Unable to retrieve";
}
}