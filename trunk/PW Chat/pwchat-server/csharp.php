<?php
//required... don't want text/html passed
header("Content-type: text/plain");
//start a session
//include the users file
require_once("users.php");
//include marshalling.php for server communication
require_once("marshalling.php");
//set errors to plain text
ini_set("html_errors", false);
//disable all error reporting (for production use only)
if(isset($_GET['noerr'])){
	error_reporting(0);
} else {
	error_reporting(E_ALL);
}
//these should be the same default keys as the ones in the client
define("DEFAULTKEY", "jJTg6z00UFmHMpGLyTgHAjdcrWfzrEPZ9fbKRpLOVP0=");
define("DEFAULTIV", "euo++R62pY940xlUPeMrAu+lPCL4/etfzf3RyQFUKos=");
//you can put anything you want here or generate one @ http://shrtrl.tk/keygen
//this is sent on login to auth of requests
//STRONGLY RECOMENDED TO CHANGE AS THIS IS JUST LIKE A PASSWORD
define("AUTHKEY", "JI1hEq8MKTLdXZLdfjtJOIl6vj42UFmtdeAIuzl5");
//force the use of encryption? if false encryption will still
//be used if client sends encrypted data
//I do recomended leaving this as true for obvious security reasons
define("ENCRYPTION", true);

define("LOGLOC", "/PWServer/logservice/logs/world2.chat");
//define("LOGLOC", "world2.chat");

define("SQLTABLE", "CREATE TABLE IF NOT EXISTS `messages` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '-1',
  `type` text NOT NULL,
  `chldst` int(11) NOT NULL DEFAULT '0',
  `time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `message` text NOT NULL,
  PRIMARY KEY (`id`),
  FULLTEXT KEY `message` (`message`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1;"
);
define("SQLUSER", "root");
define("SQLPASS", "pass");
define("SQLHOST", "localhost");
define("SQLDB", "pw_chat");
define("DELIVERYDH", "192.168.1.10");
define("DELIVERYDP", 29100);
define("GAMEDBDH", "192.168.1.10");
define("GAMEDBDP", 29400);

///////////////////////////////////////////////////////////////////////////////
//request functions section

//if ?test is passed run the keytest
if($_GET['test'] == AUTHKEY){
	$key = readkeyfile();
	$enc = encrypt("Hello World", $key[0], $key[1]);
	$dec = decrypt($enc, $key[0], $key[1]);
	echo "Encrypted:$enc\n";
	echo "Decrypted:$dec\n";
}
//run json formation test
if($_GET['jtest'] == AUTHKEY){
	$enc = formjson('{"message" : "Hello World"}', false);
	$dec = readjson($enc);
	echo "JSON Message:$enc\n";
	echo "Original Message:".$dec['message']."\n";
}
//initialize db if it isn't...
if($_GET['initdb'] == AUTHKEY){
	if(initdb()){
		echo "Database intialization complete";
	} else {
		echo "Database initialization failed";
	}
}
//Client requests begin (they all use post)
if(isset($_GET['login'])){
	$d = readjson($_POST['json']);
	$t = login($d['username'], $d['password']);
	if($t[0]){
		$aiv = genkey(); $aiv = $aiv[0];
		echo formjson('{"login" : 1, "aid" : "'. AUTHKEY .'", "aiv" : "'. $aiv. '",  "salt" : "'.saltgen().'"}');
	} else {
		echo formjson('{"login" : 0, "salt" : "'.saltgen().'"}');
	}
	//$t = $t ? "True" : "False";
	//fwrite(fopen("test.txt", "a"), $_POST['json']."$t\n");
}

$k = readkeyfile();
if(decrypt($_GET['auth'], $k[0], $_GET['aiv']) == AUTHKEY){
	unset($k); //don't need key anymore
	/* server doesn't keep track of auth anymore
	if(isset($_GET['logout'])){
		$d = readjson($_POST['json']);
		if($d['logout']){
			echo formjson('{"logout" : 1, "salt" : "'.saltgen().'"}');
		}
	}*/
	$encryption = (bool)substr($_POST['json'], 1, 1);
	if(isset($_GET['sendmsg'])){
		$d = readjson($_POST['json']);
		//var_dump($d);
		//var_dump($_POST);
		//broadcast($_GET['msg']);
		$c = false;
		if($d){
			$t = login($d['username'], $d['password']);
			if($t[0] && $t[1]){
				if(broadcast($d['msg'])){
					echo formjson('{"broadcast" : 1, "salt" : "'.saltgen().'"}');
					$q = "values (-1, 'Chat', '9', '".baseb64_encode($d['msg'])."')";
					$db = mysqli_connect(SQLHOST, SQLUSER, SQLPASS, SQLDB);
					if($db){
						mysqli_query($db, "insert into  `messages` (`uid`, `type`, `chldst`, `message`) $q;");
					}
					$c = true;
				}
			}
		}
		if(!$c){
			echo formjson('{"broadcast" : 0, "salt" : "'.saltgen().'"}');
		}
	}
	if(isset($_GET['getmsgs'])){
		$d = readjson($_POST['json']);
		if($d){
			//client keeps track of id's
			echo formjson(getchats($d["num"], $d["limit"]));
		}
	}
	if(isset($_GET['getrolename'])){
		$d = readjson($_POST['json']);
		if($d){
			$name = getrolename($d['uid']);
			echo formjson('{"rolename" : "'. $name .'", "salt" : "'.saltgen().'"}');
		}
	}
}
//var_dump(readjson(formjson('{"broadcast" : 0, "salt" : "'.saltgen().'"}')));
//end request functions section
///////////////////////////////////////////////////////////////////////////////
log2db();

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

//the add and strip padding I believe are fully PKCS7 compliant...
//if something weird ever comes up let me know...

//blocksize is in bytes not bits (like it is in C#)
function addpadding($string, $blocksize = 32){
	$len = strlen($string);
	$pad = $blocksize - ($len % $blocksize);
	$string .= str_repeat(chr($pad), $pad);
	return $string;
}
function strippadding($string){
	$slast = ord(substr($string, -1));
	$slastc = chr($slast);
	$pcheck = substr($string, -$slast);
	if(preg_match("/$slastc{".$slast."}/", $string)){
		$string = substr($string, 0, strlen($string)-$slast);
		return $string;
	} else {
		return false;
	}
}
function readkeyfile($keyfile = "key/key.key"){
	$file = str_replace("\r\n", "", @file($keyfile));
	return $file;
}
function genkey(){
	//yes I know it says mcrypt_create_iv
	//it really doesn't matter and php doesn't have mcrypt_create_key
	//and because the iv has to be the same size as key anyway
	$r[0] = base64_encode(mcrypt_create_iv(32));
	$r[1] =  base64_encode(mcrypt_create_iv(32));
	return $r;
}
//server isn't supposed to be writing keyfiles anyway
function writenewkeyfile($keyfile = "key/key.key"){
	$h = fopen($keyfile, "w");
	fwrite($h, implode("\r\n", genkey())."\r\n");
}
function encrypt($data, $key, $iv){
	//client should format it with windows line endings
	$key = $key ? base64_decode($key) : base64_decode(DEFAULTKEY);
	$iv = $iv ? base64_decode($iv) : base64_decode(DEFAULTIV);
	return base64_encode(mcrypt_encrypt(MCRYPT_RIJNDAEL_256, $key, addpadding($data), MCRYPT_MODE_CBC, $iv));
	
}
function decrypt($data, $key = null, $iv = null){
	$key = $key ? base64_decode($key) : base64_decode(DEFAULTKEY);
	$iv = $iv ? base64_decode($iv) : base64_decode(DEFAULTIV);
	return strippadding(mcrypt_decrypt(MCRYPT_RIJNDAEL_256, $key, base64_decode($data), MCRYPT_MODE_CBC, $iv));
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
function login($username, $password){
	global $uhash, $user;
	/*plain text is ALWAYS sent by the client BUT
	is encrypted if enabled (and it should be)
	this hash is just here to make server and client
	see the same thing for actual comparison*/
	if($uhash != "text/plain"){
		$password = hash($uhash, $password);
	}
	$uu = false;
	$i = 0;
	$uuid = 0;
	foreach($user as $u){
		//figure out if the username passed actually exists
		if($username == $u['username']){
			$uu = true;
			$uuid = $i;
		}
		$i++;
	}
	if($uu && ($user[$uuid]['password'] == $password)){
		$r[0] = true;
		$r[1] = $user[$uuid]['bcast'];
		return $r;
	}
	return array(0 => false, 1 => false);
}
//$from is id to start from, default is 100 from last
//$limit is how many to give from $from if -1 it reads to end
function getchats($cid = -1, $from = -100, $limit = -1){
	$db = mysqli_connect(SQLHOST, SQLUSER, SQLPASS, SQLDB);
	if($db){
		if($cid == -1){
		$q = mysqli_fetch_row(mysqli_query($db, "select `id` from `messages` order by `id` desc"));
		$q = $q[0]+$from; $q++; //+- is weird but it works as - would be +$from due to --
							//$q++ is because of how the indexes work}
			if($limit == -1){
				$query = "select * from `messages` where `id` >= $q";
			} else {
				$query = "select * from `messages` limit $q, $limit";
			}
		} else {
			$cid++; //same reason as $q++
			if($limit == -1){
				$query = "select * from `messages` where `id` >= $cid";
			} else {
				$query = "select * from `messages` limit $cid, $limit";
			}
		}
		$que = mysqli_query($db, $query);
		if($que){
			while($qq = mysqli_fetch_row($que)){
				$qqq[] = array( 'cid' => $qq[0],
								'uid' => $qq[1],
								'type' => $qq[2],
								'chldst' => $qq[3],
								'time' => $qq[4],
								'msg' => addslashes(base64_decode($qq[5])));
			}
			if($qqq){
				return json_encode($qqq);
			} else {
				
			}
		}
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
//echo getrolename(32);
//var_dump(readjson(getchats(204978)));
//var_dump(readjson(getchats(100000)));
//var_dump(broadcast("Broadcast System Message"));
// $tt = formjson('{"test" : "▲▲▲♂♂♂"}');
// var_dump($tt);
// $t = readjson($tt);
// var_dump(broadcast($t['test']));
// var_dump(base64_encode("▲"));
// $k = readkeyfile();
 // var_dump(decrypt("Oy5SJuhzF/kDmQrwfZnqhSufge2o1AA6UdZNtWga4I0=", $key[0], "9oXbBR/7DX+M+jSwTYvCJblpmLvy4htMK5SELvgTpQY="));
 //var_dump(base64_encode(hash("sha512", iconv("UTF-8", "UCS-2LE", "▲▲▲♂♂♂"), true)));
 //var_dump(base64_encode(hash("sha512", "▲▲▲♂♂♂", true)));