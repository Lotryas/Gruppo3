﻿function controlla_login()
{
    if (document.getElementById('email').value.length < 2) {
        alert("L' EMAIL " + document.getElementById('email').value + " CHE HAI INSERITO E' TROPPO BREVE");
        return false;
    }

    if (document.getElementById('pass').value.length < 8) {
        alert(" LA PASSWORD ISERITA E' TROPPO BREVE");
        return false;
    }

}

function controlli_registrazione()
{
    
    if (document.getElementById('nome').value.length < 2 ||
        document.getElementById('nome').value.length > 50) {
        alert("Il nome utente inserito non è valido");
        return false;
    }

    if (document.getElementById('email').value.length < 3 ||
        document.getElementById('email').value.length > 255) {
        alert("L'email inserita non è valida");
        return false;
    }


    let p1 = document.getElementById('password1').value;
    let p2 = document.getElementById('password2').value;

  

    if (p1 != p2) {
        alert("LE PASSWORD NON CORRISPONDONO");
        return false;
    }

    let error = [];

    if (p1.length < 8) {
        error.push("La password deve avere almeno 8 caratteri");
    }
    if (p1.search(/[a-z]/) < 0) {
        error.push("La password deve contenere almeno una lettera minuscola");
    }
    if (p1.search(/[A-Z]/) < 0) {
        error.push("La password deve contenere almeno una lettera maiuscola");
    }
    if (p1.search(/[0-9]/) < 0) {
        error.push("La password deve contenere almeno un numero");
    }

    if (error.length > 0) {
        alert(error.join("\n"));
        return false;
    }



    return true;

}