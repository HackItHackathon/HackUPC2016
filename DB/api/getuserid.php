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

$sql = "SELECT id FROM users WHERE nom = '" . $_GET['nom'] . "'";

//echo $sql;

$result = $conn->query($sql);


if(mysqli_num_rows($result) == 0){
	$sql = "INSERT INTO users (`nom`) VALUES ('" . $_GET['nom'] . "')" ;
	$result = $conn->query($sql);
	$sql = "SELECT id FROM users WHERE nom = '" . $_GET['nom'] . "'";
	$result = $conn->query($sql);
	$row= $result->fetch_assoc();
	$arr = array('id' =>  $row['id']);
	echo json_encode($arr);
	
} else {
	$row= $result->fetch_assoc();
	$arr = array('id' =>  $row['id']);
	echo json_encode($arr);	
}

/*

$arr = array('id' => 1);

echo json_encode($arr);*/

?>