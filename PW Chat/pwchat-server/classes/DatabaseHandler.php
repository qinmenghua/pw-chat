<?php

class DatabaseHandler{
	define("MYSQLTABLE", "CREATE TABLE IF NOT EXISTS `messages` (
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
}