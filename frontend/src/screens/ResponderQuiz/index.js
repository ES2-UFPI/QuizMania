import React, { useState, useEffect } from "react";
import { Text, View } from "react-native";
import { Container, Header, Pergunta } from "../../../components";

export default function responderQuiz() {
  const [perguntas, setPerguntas] = useState([]);
  const [perguntaAtual, setPerguntaAtual] = useState(undefined);
  const respostas = {};

  useEffect(() => {
    const data = [
      {
        titulo: "Qual é o valor de PI?",
        alternativa1: "3.14",
        alternativa2: "3",
      },
      {
        titulo: "Qual é o melhor curso?",
        alternativa1: "Computação",
        alternativa2: "Música",
      },
    ];
    setPerguntas(data);
    setPerguntaAtual(0);
  }, []);

  function responderPergunta(pergunta, resposta) {
    alert("Resposta para a pergunta " + pergunta + ": Alternativa " + resposta);
    respostas[pergunta] = resposta;
    if (perguntaAtual < perguntas.length - 1)
      setPerguntaAtual(perguntaAtual + 1);
    else alert("Não tem mais pergunta");
  }

  return (
    <Container>
      <Header />
      {perguntaAtual != undefined && (
        <React.Fragment>
          {perguntaAtual > 0 && (
            <Text
              onPress={() => setPerguntaAtual(perguntaAtual - 1)}
              style={{ marginTop: 10, fontSize: 16 }}
            >
              {" "}
              {"< "} Pergunta Anterior
            </Text>
          )}
          <Pergunta
            data={perguntas[perguntaAtual]}
            perguntaAtual={perguntaAtual + 1}
            responder={responderPergunta.bind(this)}
          />
        </React.Fragment>
      )}
    </Container>
  );
}
