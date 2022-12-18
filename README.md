# MoneyPlus
Repositorio: https://github.com/pedroamaro89/MoneyPlus

Projeto com todos os requisitos efetuados.

No R9, se não existir a pasta C:/temp, é criada ao correr o projeto. Na primeira vez que é corrido o programa é serializado o ficheiro c:/temp/categories.yaml, se o mesmo já existir, desserializa.

No R10, não consegui enviar emails devido a configurações com o SMTP, por isso tal como referido no enunciado, criei a tabela EmailLogs onde é simulado o envio de email a cada 24h.

Para criar um administrador tem de aceder ao link: admin/secret?pwd=pouco-segura. Se a password falhar redireciona para página de erro e regista o erro no log.