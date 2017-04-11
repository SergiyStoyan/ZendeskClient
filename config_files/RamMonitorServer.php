<?php

//print_r("test");
//file_put_contents("RamMonitorLog.txt", json_encode($_POST));
//file_put_contents("RamMonitorLog2.txt", $_POST);
//$req_dump = print_r($_REQUEST, TRUE);
//file_put_contents("RamMonitorLog2.txt", $req_dump);

$header = print_r(apache_request_headers(), true);
$body = file_get_contents('php://input');
file_put_contents("RamMonitorLog1.txt", "$header\r\n\r\n$body");

$object = json_decode($body, true);
if($object === null) $object = "NULL";
elseif(!$object) $object = "EMPTY";
file_put_contents("RamMonitorLog2.txt", print_r($object, true));



?>
