// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Função para exibir o toast

document.addEventListener("DOMContentLoaded", function() {
    function showSuccessToast(message) {
        // Altera o texto da mensagem do toast
        document.getElementById('toastMessage').innerText = message;

        // Exibe o toast
        var toastEl = document.getElementById('successToast');
        var toast = new bootstrap.Toast(toastEl);
        toastEl.style.display = 'block';
        toast.show();
    }
});

