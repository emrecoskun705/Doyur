function OpenActivation() {
    Swal.fire({
        title: 'Aktivasyon kodu',
        text: 'Lütfen mailinize gelen kodu giriniz',
        input: 'text',
        confirmButtonText: 'Onayla',
        showLoaderOnConfirm: true,
        allowOutsideClick: () => !Swal.isLoading(),
        preConfirm: (activation) => {
            return $.ajax({
                type: 'POST',
                url: '/login.aspx/Activation',
                data: JSON.stringify({
                    code: activation
                }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (r) {
                    if (r.d == true) {
                        window.location.href = "/";
                        return true;
                    } else {
                        Swal.showValidationMessage(
                            "Hatalı Kod"
                        );
                        return false;
                    }
                }
            })
        }
    });
}