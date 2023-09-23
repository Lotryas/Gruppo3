function controllaDati()
{
    let errore = [];
    if (document.getElementById("titolo").value.length < 2 ||
        document.getElementById("titolo").value.length > 255)
        errore.push("Il titolo inserito non è valido");

    if (document.getElementById("autore").value.length < 2 ||
        document.getElementById("autore").value.length > 200)
        errore.push("L'autore inserito non è valido");

    if (document.getElementById("genere").value.length < 2 ||
        document.getElementById("genere").value.length > 100)
        errore.push("Il genere inserito non è valido");

    if (document.getElementById("quantita").value < 0 ||
        document.getElementById("quantita").value == "")
        errore.push("La quantità inserita non è valida");

    if (document.getElementById("nomefile").value.length < 2 ||
        document.getElementById("nomefile").value.length > 255)
        errore.push("Il nome file inserito non è valido");

    if (errore.length > 0)
    {
        alert(errore.join("\n"));
        return false;
    }
        
    return true;

}