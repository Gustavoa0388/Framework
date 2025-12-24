📘 README_INPUTS.md

GS Core UI — Inputs & Controles de Estado

🎯 Objetivo

Este documento define o contrato oficial dos inputs do GS Core UI, explicando:

Tipos de inputs existentes

Arquitetura base (GsInputBase)

Diferença entre inputs baseados em TextBox e inputs compostos

Regras obrigatórias de implementação

Boas práticas de uso

📌 Este documento é normativo.
Novos inputs devem seguir estas regras.

🧱 Arquitetura Base — GsInputBase

GsInputBase é a base de TODOS os inputs interativos do GS Core UI.

Ele fornece:

Pipeline de UX

Integração com Theme (GsTheme)

Validação (Required)

Estado visual (Focus, Error)

API previsível para Forms Base

Ele NÃO assume:

Que todo input edita texto

Que todo input usa TextBox

👉 O controle interno pode ou não existir.

🧠 Classificação Oficial de Inputs

Os inputs do GS Core UI são divididos em DOIS grupos oficiais:

🟦 1. Inputs Baseados em TextBox
Características

Possuem edição textual

Criam um TextBoxBase interno

Sobrescrevem CreateInnerTextBox()

InnerTextBox nunca é null

Controles desse grupo
Controle	Descrição
GsTextBox	Texto simples
GsMaskedTextBox	Texto com máscara
GsNumericTextBox	Entrada numérica
GsDateTextBox	Datas
GsPasswordBox	Senhas
Regras obrigatórias

✔️ Deve sobrescrever CreateInnerTextBox()
✔️ Deve retornar um TextBoxBase válido
✔️ Pode confiar em InnerTextBox

🟩 2. Inputs Compostos (SEM TextBox)
Características

Não editam texto diretamente

Criam controles reais no construtor

NÃO sobrescrevem CreateInnerTextBox()

InnerTextBox é sempre null

Controles desse grupo
Controle	Tipo
GsComboBox	Seleção
GsCheckBox	Estado booleano
GsRadioOption	Seleção única
GsToggleSwitch	Alternância On/Off
Regras obrigatórias

❌ NÃO sobrescrever CreateInnerTextBox()
✔️ Criar controle real no construtor
✔️ Usar GsInputBase apenas como container de UX

🧠 Regra de Ouro do GS Core UI

GsInputBase é um pipeline de UX,
não um TextBox disfarçado.

Se o input:

Edita texto → TextBox-based

Representa estado ou escolha → Composto

✅ Validação (Required)

Todos os inputs podem usar:

Required = true;
RequiredMessage = "Campo obrigatório";

Comportamento:

A validação é chamada automaticamente

Inputs compostos implementam sua própria regra

O erro visual é tratado pelo GsInputBase

🎨 Tema (GsTheme)

Todos os inputs:

Devem implementar ApplyTheme

Devem usar somente tokens do GsTheme

Nunca usar cores hardcoded

❌ Anti-padrões (PROIBIDOS)

🚫 Criar input que não herda de GsInputBase
🚫 Sobrescrever Items de controles WinForms
🚫 Assumir InnerTextBox em inputs compostos
🚫 Lógica de negócio dentro do input
🚫 Acesso a banco ou serviços

🧪 Checklist para novo input

Antes de criar um novo input, responda:

Ele edita texto?

Ele representa estado?

Ele precisa de TextBoxBase?

Ele respeita o Theme?

Ele funciona com Required?

Ele não conhece regra de negócio?

Se alguma resposta for “não sei”, rever o design.

📌 Exemplos de Uso
Input Textual
var txtNome = new GsTextBox
{
    Required = true,
    Placeholder = "Nome"
};

Input Composto
var chkAtivo = new GsCheckBox
{
    Text = "Ativo",
    Required = true
};

🏁 Conclusão

O sistema de inputs do GS Core UI foi projetado para:

Ser previsível

Ser extensível

Ser fácil de manter

Evitar bugs de ciclo de vida

Facilitar a vida de quem consome o framework

📦 Seguir este documento é obrigatório para manter a consistência do GS Core UI.