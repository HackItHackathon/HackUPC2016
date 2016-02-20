<?php


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

$sql = "UPDATE users SET ida='0' WHERE id = '". $_GET['id'] ."'";

echo $sql;

$result = $conn->query($sql);

?>