<?php
//any hash supported by php can be used as uhash, even text/plain
$uhash = "text/plain";
$user[0]['username'] = "user";
$user[0]['password'] = "pass";
$user[0]['bcast'] = true;
//bcast is used to allow or limit users from broadcasting messages
//so you can allow people to view but not send anything
//useful for GMs you don't want speaking for w/e reason

//new users can be added by changing the uid
//for example
//$user[1]['username'] = "myUser";
//$user[1]['password'] = "myPass";
//$user[1]['bcast'] = true;