<?php

//echo $_GET['nom'];

$servername = "localhost";
$username = "hackathon";
$password = "hackit";
$database = "hackathon";
// Create connection
$conn = new mysqli($servername, $username, $password, $database);

// Check connection
if ($conn->connect_error) {
	die("Connection failed: " . $conn->connect_error);
}
//echo "Connected successfully";


//INSERT INTO `games`(`ida`,`idd`) VALUES (4,5)

$sql = "INSERT INTO `games`(`ida`,`idd`) VALUES ('" . $_GET['ida'] . "','". $_GET['idd'] ."')" ;
$result = $conn->query($sql);

//echo $sql;

//SELECT `gameid`  FROM `games` WHERE `ida` = 4 and `idd` = 5 and `punta` = -1 and `puntd` = -1
$sql = "SELECT gameid FROM games WHERE ida = '" . $_GET['ida'] . "' AND idd = '".$_GET['idd'] .
"' AND punta = '-1' AND puntd = '-1'";


//echo $sql;

$result = $conn->query($sql);


$row= $result->fetch_row();
$arr = array('gameid' =>  $row[0]);
echo json_encode($arr);


?>