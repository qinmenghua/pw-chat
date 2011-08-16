<?php
//Paste the code given by client here
//Example of what it should look like
//define("ENCRYPTIONKEY", "1UiAeAf+iG7sNR4NPh9MRZ6wC3sTRfF2PjwQgQBhSZQ=");
//define("ENCRYPTIONIV", "ioP7YFEDzPHtq9kosZrya6FgRP/ne1uWih3kgYBTErA=");



//SQL Settings
//Supported SQLTYPE's are mysql and sqlite
define("SQLTYPE", "mysql");
//<mysql>
//hint: ^ means ignore this area if using sqlite
define("MYSQLUSER", "root");
define("MYSQLPASS", "5609903591");
define("MYSQLHOST", "localhost");
define("MYSQLDB", "pw_chat");
//</mysql>
//<sqlite>
//hint: ^ means ignore this area if using mysql
//Path to SQLite database, will be created if it doesn't exist
//recommended 
define("SQLITEDB", "key\database.db");
//</sqlite>

//PW Server info
define("DELIVERYDH", "192.168.1.10");
define("DELIVERYDP", 29100);
define("GAMEDBDH", "192.168.1.10");
define("GAMEDBDP", 29400);

//define("LOGLOC", "/PWServer/logservice/logs/world2.chat");
define("LOGLOC", "world2.chat");

/******************************ADVANCED SETTINGS******************************/
/**DO NOT MODIFY ANYTHING BELOW THIS LINE UNLESS YOU KNOW WHAT YOU ARE DOING**/

//these should be the same default keys as the ones in the client source code
//unless you downloaded compiled etc then don't touch
define("DEFAULTKEY", "jJTg6z00UFmHMpGLyTgHAjdcrWfzrEPZ9fbKRpLOVP0=");
define("DEFAULTIV", "euo++R62pY940xlUPeMrAu+lPCL4/etfzf3RyQFUKos=");

//force the use of encryption? if false encryption will still
//be used if client sends encrypted data
//I do recomended leaving this as true for obvious security reasons
define("ENCRYPTION", true);

//you can put anything you want here or generate one @ http://shrtrl.tk/keygen
//this is sent on login to auth of requests
//STRONGLY RECOMENDED TO CHANGE AS THIS IS JUST LIKE A PASSWORD
define("AUTHKEY", "JI1hEq8MKTLdXZLdfjtJOIl6vj42UFmtdeAIuzl5");
//^ probably going to make this defunct in favor of something else