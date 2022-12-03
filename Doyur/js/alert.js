function SendAlert(type, message) {
    switch (type) {
        case 'Success':
            cssclass = 'alert alert-success';
            durum = "Başarılı";
            break;
        case 'Error':
            cssclass = 'alert alert-danger';
            durum = "Hata";
            break;
        case 'Warning':
            cssclass = 'alert alert-warning';
            durum = "Hata";
            break;
        default:
            cssclass = 'alert alert-info';
            durum = "Bilgi";
            break;
    }

    var a = "<div class='" + cssclass + " ><strong> " + durum + "!</ strong > " + message + "</div>";
    console.log(a);

    $('#alert-message').append("<div class='" + cssclass + " ' id='alert'><strong> </ strong > " + message + "<span class='closebtn' >&times;</span></div>");

    var close = document.getElementsByClassName("closebtn");
    var i;

    // Loop through all close buttons
    for (i = 0; i < close.length; i++) {
        // When someone clicks on a close button
        close[i].onclick = function () {

            // Get the parent of <span class="closebtn"> (<div class="alert">)
            var div = this.parentElement.parentElement;

            // Set the opacity of div to 0 (transparent)
            div.style.opacity = "0";

            // Hide the div after 600ms (the same amount of milliseconds it takes to fade out)
            div.remove();
        }
    }
}

