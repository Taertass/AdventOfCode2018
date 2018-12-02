Hvordan mon kodenissen klarer den her?

<html><head><meta charset="UTF-8">

</head><body>

<script>
	function reverseString(input) {
		return input.split('').reverse().join('');
	}

	function OnClick()
	{
		var flagValue = document.getElementById("flagInput").value;

		if (reverseString(flagValue) == '}tlepmis_aas{3CN')
		{
			alert('Du fandt flaget');
		}
		else
		{
			alert('Prøv igen');
		}
	}
</script>


<input id="flagInput" value="Indtast flaget:" type="text">
<input onclick="OnClick();" type="button">



</body></html>