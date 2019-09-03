
var xhr = new XMLHttpRequest();
xhr.open('GET', 'list.html', false);
xhr.send();
if (xhr.status == 200) {
    var test = xhr.responseText.replace('<a href="index.html">Вернуться.</a>', "<br>");
    document.write(test);
}