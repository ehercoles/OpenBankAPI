const uri = 'api/Movimentacao';
let movimentacoes = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function sacar() {
    const txtValor = document.getElementById('txtValor');
    txtValor.value = -txtValor.value;
    depositar();
}

function depositar() {
    const txtValor = document.getElementById('txtValor');
    const valor = Number(txtValor.value.trim());
    let saldoAnterior = 0;

    if (movimentacoes.length > 0) {
        const previousItem = movimentacoes[movimentacoes.length - 1];
        saldoAnterior = previousItem.saldo;
    }

    const item = {
        valor: valor,
        saldo: saldoAnterior + valor
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            txtValor.value = '';
        })
        .catch(error => console.error('Erro ao movimentar conta.', error));
}

function _displayCount(itemCount) {
    const countLabel = itemCount === 1 ? 'movimentação' : 'movimentações';
    document.getElementById('counter').innerText = `${itemCount} ${countLabel}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('movimentacoes');
    tBody.innerHTML = '';
    _displayCount(data.length);

    data.forEach(item => {
        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(item.valor.toLocaleString('pt-BR'));
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        textNode = document.createTextNode(item.saldo.toLocaleString('pt-BR'));
        td2.appendChild(textNode);
    });

    movimentacoes = data;
}
