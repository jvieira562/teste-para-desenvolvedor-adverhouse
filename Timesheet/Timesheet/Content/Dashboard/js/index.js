var lancamentoClicado = 0;

var aprovadores = [];

$(document).ready(function () {
    AtribuirAcoes();
    AbrirModalNovoJob();
    BuscarUsuariosNaoAprovadores();
    btnBoxAprovacaoClicado();
});


function UsuarioLogadoEstaNaListaDeAprovadores() {
    var usuarioEAprovador = false;
    var boxAprovacao = $('.box-aprovacao');
    boxAprovacao.hide();
    usuarioLogado = usuarioLogado;
    //console.log("\nUSUARIO LOGADO ID: " + usuarioLogado)
    //console.log('CHAMADA AJAX')

    $.ajax({
        url: '/Aprovador/BuscarAprovadoresDoLancamento',
        type: 'GET',
        dataType: 'json',
        data: { LancamentoId: lancamentoClicado },
        success: function (data) {
            data.forEach(function (usuario) {
                aprovadores.push(usuario.UsuarioId);                
            });

            if (aprovadores.includes(usuarioLogado)) {
                //console.log('APROVADORES INCLUDES: ' + usuarioLogado)
                //console.log(aprovadores)
                usuarioEAprovador = true;
            }
            aprovadores = [];

            if (usuarioEAprovador) {
                boxAprovacao.show();
            } else {
                boxAprovacao.hide();
            }

            hideLoading();
        },
        error: function () {

        }
    });
}

function AtribuirAcoes() {

    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    });
    $('.btn-adicionar-aprovador').click(function () {
        showLoading();
    })
    $('.btn-fechar-modal-aprovadores').click(function () {
        $('.modal-aprovadores').toggleClass('d-none');
        $('.modal-body-usuario').remove();
        aprovadores = [];
    });

}
function AbrirModal(lancamentoId, usuarioId, statusLancamento) {
    showLoading();
    console.log('abrindo modal');
    lancamentoClicado = lancamentoId;
    usuarioLogadoNaSessao = usuarioId;
    
    var modal = $('.modal-aprovadores');
    var modalBody = $('.modal-body');
    modalBody.find('h4').remove();
    var forms = modal.find('#formsAdicionarAprovador');
    forms.hide();

    if (usuarioId == usuarioLogado && statusLancamento != 'REPROVADO') {

        forms.show();
        var inputLancamento = forms.find('#inLancamentoId');
        inputLancamento.val(lancamentoId);
    }
    if (statusLancamento == 'NAO_VALIDADO') {
        
        modalBody.append('<h4>Não validado!</h4>')
    }
    $('.modal-body-usuario').remove();

    modal.toggleClass('d-none');

    UsuarioLogadoEstaNaListaDeAprovadores();
    BuscarAprovadoresDoLancamento(lancamentoId, usuarioId);
    BuscarUsuariosNaoAprovadores();
    hideLoading();
}
function BuscarAprovadoresDoLancamento(lancamentoId, usuarioId) {

    var container = $('.modal-body');
    var StatusAprovador = {
        REPROVADO: 1,
        PENDENTE: 2,
        APROVADO: 3
    };

    $.ajax({
        url: '/Aprovador/BuscarAprovadoresDoLancamento',
        type: 'GET',
        dataType: 'json',
        data: { LancamentoId: lancamentoId },
        success: function (data) {
            data.forEach(function (usuario) {

                aprovadores.push(usuario.UsuarioId);
                var statusNome = Object.keys(StatusAprovador).find(function (key) {
                    return StatusAprovador[key] === usuario.Status;
                });
                var textHtml = '<div class="modal-body-usuario"><p>' + usuario.Nome + '</p><p><i class="fa-solid fa-arrow-right fa-sm"></i></p><p>' + statusNome + '</p><div class="box-circulo-status"><div class="' + statusNome + '" style="min-height: 18px; min-width: 18px; border-radius: 100%;" data-toggle="tooltip" data-placement="right" title="' + statusNome + '"></div></div></div>';
                if (usuario.Status == StatusAprovador.PENDENTE && usuarioId == usuarioLogado) {
                    textHtml = '<div class="modal-body-usuario"><p>' + usuario.Nome + '</p><p><i class="fa-solid fa-arrow-right fa-sm"></i></p><p>' + statusNome + '</p><div class="box-circulo-status"><i class="fa-solid fa-trash btn-remover-aprovador" data-toggle="tooltip" data-placement="right" title="Remover aprovador" data-aprovador-id="' + usuario.UsuarioId + '"></i></div>';
                } 

                container.append(textHtml);

            });
            RemoverAprovador();
        },
        error: function () {

        }
    });
    
} 
function BuscarUsuariosNaoAprovadores() {
    
    $('.box-select').one('click', function () {

        showLoading();
        var select = $(this).find('select');
        select.empty();
        $.ajax({
            url: '/Usuario/BuscarNaoAprovadores',
            type: 'GET',
            dataType: 'json',
            data: { lancamentoId: lancamentoClicado },
            success: function (data) {
                select.empty();
                data.forEach(function (obj) {

                    select.append($('<option>', {
                        value: obj.UsuarioId,
                        text: obj.Nome,
                        'data-usuario-id': obj.UsuarioId,
                    }));
                });
                select.trigger('change');
                hideLoading();
            },
            error: function () {
                // Tratar erros, se necessário
            }
        });

    });
}
function AbrirModalNovoJob() {

    var modalNovoJob = $('#modalNovoJOb');
    var btnFecharModal = $('.btn-fechar-modal-novo-job').click(function () {
        modalNovoJob.toggleClass('d-none');
    });
    var btnAbrirModal = $('#btnNovoJob').click(function () {
        modalNovoJob.toggleClass('d-none');
    });
}
function RemoverAprovador() {
    $('.btn-remover-aprovador').click(function () {
        showLoading();
        var btnRemoverAprovadorClicado = $(this);
        var aprovadorId = this.dataset.aprovadorId;

        $.ajax({
            url: '/Aprovador/RemoverAprovador',
            type: 'POST',
            dataType: 'json',
            data: {
                AprovadorId: aprovadorId,
                LancamentoId: lancamentoClicado
            },
            success: function (textStatus) {
                btnRemoverAprovadorClicado.closest('.modal-body-usuario').remove();
                hideLoading();
                createAlert('Aviso importante!', 'O aprovador selecionado foi removido da lista!', 'Você pode adicionar novamente o aprovador, se necessário.', 'success', true, true, 'pageMessages');
            },
            error: function (textStatus) {
                hideLoading();
            }
        });
    });
}

function btnBoxAprovacaoClicado() {

    $('.btn-aprovar-lancamento').click(function () {
        AprovarOuReprovarLancamento(3)
    });
    $('.btn-reprovar-lancamento').click(function () {
        AprovarOuReprovarLancamento(1)
    });
}

function AprovarOuReprovarLancamento(status) {
    console.log('\nFunção de aprovar ou reprovar foi chamada!')
    $('.modal-body-usuario').remove();
    showLoading();
    $.ajax({
        url: 'Aprovador/AprovarOuReprovarLancamento',
        type: 'POST',
        dataType: 'json',
        data: {
            AprovadorId: usuarioLogado,
            LancamentoId: lancamentoClicado,
            Status: status
        },
        success: function () {
            createAlert('Aprovação Registrada!', 'Sua aprovação foi registrada com sucesso.!', 'Aguarde as demais aprovações para finalizar o processo.', 'info', true, false, 'pageMessages');
            BuscarAprovadoresDoLancamento(lancamentoClicado, usuarioLogado);
            hideLoading();
        },
        error: function () {
            createAlert('Aprovação não Registrada!', 'Sua aprovação não foi registrada.!', 'Se o problema persistir, contate um administrador.', 'danger', true, false, 'pageMessages');
            hideLoading();
        }
    });
}