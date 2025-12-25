# Diagnóstico Visual (Visual Diagnostics)
GS Core UI

---

## O que é Diagnóstico Visual

Diagnóstico Visual é o mecanismo do GS Core UI que permite **exibir informações técnicas sob demanda**, 
sem comprometer a experiência do usuário, segurança ou governança do sistema.

⚠️ Diagnóstico Visual **não é logging**  
⚠️ Diagnóstico Visual **não persiste dados**  
⚠️ Diagnóstico Visual **não captura exceções**

Ele apenas **exibe visualmente** informações técnicas **fornecidas explicitamente pela aplicação**.

---

## Princípios Fundamentais

### Separação de Públicos

| Público | Informação exibida |
|------|------------------|
Usuário final | Mensagem clara e amigável |
Suporte / TI | Detalhes técnicos sob demanda |

Nunca misturar. Nunca inferir. Nunca exibir automaticamente.

---

### Nenhuma Automação Implícita

No GS Core UI:
- Diagnóstico não aparece automaticamente
- Existir detalhe técnico **não significa** exibir
- A aplicação **precisa autorizar explicitamente**

Governança > conveniência.

---

## Arquitetura do Diagnóstico Visual

### Modelo — GsErrorState

Responsável por **transportar informações**, nunca por decidir comportamento.

Propriedades relevantes:
- `Title` — contexto do erro
- `Message` — mensagem ao usuário
- `TechnicalDetails` — detalhe técnico
- `AllowDiagnostics` — autorização explícita de exibição

📌 Se `AllowDiagnostics == false`, o diagnóstico **nunca é exibido**, mesmo que exista.

---

### Visual — GsStateView

Responsável por **exibir o diagnóstico**, nunca por decidir sua existência.

Comportamento:
- Inline
- Expandível
- Somente leitura
- Invisível por padrão
- Botão “Detalhes técnicos” aparece apenas se:
  - `AllowDiagnostics == true`
  - `TechnicalDetails` não estiver vazio

---

### Modal de Erro

O `GsErrorModalForm`:
- Nunca exibe diagnóstico técnico
- Comunica erro final
- Encerra o fluxo

Se for necessário diagnóstico técnico → **não usar modal**, usar `GsStateView`.

---

## Quando Usar Diagnóstico Visual

Use quando:
- Falhas técnicas precisam ser analisadas
- Erros intermitentes ou contextuais
- Sistemas corporativos internos
- Ambientes de homologação ou suporte

---

## Quando NÃO Usar Diagnóstico Visual

Não use quando:
- Erro é autoexplicativo
- Informação técnica é sensível
- Fluxo é final
- Usuário não tem perfil técnico

---

## Exemplo de Uso

```csharp
try
{
    LoadClientes();
}
catch (Exception ex)
{
    stateView.ShowError(new GsErrorState(
        title: "Erro ao carregar clientes",
        message: "Não foi possível carregar os dados.",
        technicalDetails: ex.ToString(),
        allowDiagnostics: true
    ));
}
