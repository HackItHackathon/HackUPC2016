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


if(isset($_GET['punta'])){
	$sql = "UPDATE `games` SET `punta`=". $_GET['punta'] ." WHERE `gameid` = " + $_GET['gameid'] ;
	$result = $conn->query($sql);
	//SELECT * FROM games WHERE gameid = 2 AND punta != -1 AND puntd !=-1

	
}
if(isset($_GET['puntd'])){
	$sql = "UPDATE `games` SET `puntd`=" . $_GET['puntd'] . " WHERE `gameid` = " + $_GET['gameid'] ;
	$result = $conn->query($sql);
}
$sql = "SELECT * FROM games WHERE gameid = " . $_GET['gameid'] . " AND punta != -1 AND puntd !=-1";
$result = $conn->query($sql);
if(mysqli_num_rows($result) == 1){
	$row=$result->fetch_assoc();
	if($row['punta']>=$row['puntd']){
		$sql = "UPDATE users SET punt=punt + 2 WHERE id = '". $row['ida'] ."'";
		$result = $conn->query($sql);
		$sql = "UPDATE users SET punt=punt - 1 WHERE id = '". $row['idd'] ."'";
		$result = $conn->query($sql);
	}
	$sql= "DELETE * FROM games WHERE gameid = " . $_GET['gameid'] . " AND punta != -1 AND puntd !=-1";
	$result = $conn->query($sql);
}
?>