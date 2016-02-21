<?php

//echo $_GET['id'];

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

$sql = "SELECT `gameid`, `ida`,`time` FROM `games` WHERE `puntd` =-1 AND `idd` = '". $_GET['id'] . "'";
$result = $conn->query($sql);
if(mysqli_num_rows($result)>0){	
	$aux = mysqli_num_rows($result);
	
	$row= $result->fetch_assoc();
	
	$sql = "SELECT  `nom` FROM `users` WHERE `id` = ". $row['ida'];
//	echo $sql;
	$result = $conn->query($sql);
	
	$row1 = $result->fetch_assoc();
	
	$arr = array('gameid' => $row['gameid'],'partides' => $aux , 'nom' => $row1['nom'],'time' => $row['time']);
	
	echo json_encode($arr);
}
?>