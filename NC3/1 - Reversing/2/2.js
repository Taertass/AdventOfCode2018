
function test(flagValue) {
  var xorFlag = '';
  for (var i = 0; i < flagValue.length; i++) {
    xorFlag += String.fromCharCode(128 ^ flagValue.charCodeAt(i));
  }

  if (xorFlag == atob("zsOz+/Nl3+Xy3/bp3+nf5+Hu5/0=")) {
    alert('Du fandt flaget');
  }
  else {
    alert('Prøv igen');
  }

  console.log(xorFlag);
}

var xorFlagBase64 = "zsOz+/Nl3+Xy3/bp3+nf5+Hu5/0=";
var xorFlag = atob(xorFlagBase64);

console.log(xorFlagBase64);
console.log(xorFlag);

var falgValue = '';
for (var i = 0; i < xorFlag.length; i++) {
  falgValue += String.fromCharCode(xorFlag.charCodeAt(i) ^ 128)
}
console.log(falgValue);
