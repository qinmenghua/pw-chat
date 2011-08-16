<?php

class RequestHandler{
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
}