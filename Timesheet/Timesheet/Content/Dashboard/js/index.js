

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})


function AdicionarAprovador(usuarioId, lancamentoId) {
    console.log(usuarioId, lancamentoId);
}

$('.btn-fechar-modal').click(function () {
    console.log("cliqueee");
    $('.modal').toggleClass('d-none');
})

$('.celula-lancamento').click(function () {
    $('.modal').toggleClass('d-none');
})