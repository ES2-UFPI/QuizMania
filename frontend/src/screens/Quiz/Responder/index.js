import React, { useState, useEffect } from "react";
import { Text, View, FlatList } from "react-native";
import { Button } from "react-native-elements";
import { Container, Header, Pergunta, Gabarito } from "../../../../components";
import { useStyled } from "react-native-reflect";

export default function responderQuiz() {
  const [perguntas, setPerguntas] = useState([]);
  const [perguntaAtual, setPerguntaAtual] = useState(undefined);
  const [respostas, setRespostas] = useState({});
  const [gabaritoVisivel, setGabaritoVisivel] = useState(false);
  const [perguntaGabarito, setPerguntaGabarito] = useState(undefined);

  useEffect(() => {
    const data = {
      id: 1,
      questions: [
        {
          id: 1,
          correct: 3,
          text:
            "What is the answer to the meaning of life, the universe and everything?",
          answers: [
            {
              id: 1,
              text: "40",
            },
            {
              id: 2,
              text: "41",
            },
            {
              id: 3,
              text: "42",
            },
            {
              id: 4,
              text: "43",
            },
          ],
        },
        {
          id: 2,
          text: "This is a true or false question. True or False?",
          answers: [
            {
              id: 5,
              text: "True",
            },
            {
              id: 6,
              text: "False",
            },
          ],
          correct: 5,
        },
        {
          id: 3,
          text: "All options are correct. Which options are correct?",
          correct: 8,
          answers: [
            {
              id: 7,
              text: "A",
            },
            {
              id: 8,
              text: "B",
            },
          ],
        },
      ],
    };
    setPerguntas(data.questions);
    setPerguntaAtual(0);
  }, []);

  function responderPergunta(pergunta, resposta) {
    const novasRespostas = respostas;
    novasRespostas[pergunta] = resposta;
    setRespostas(novasRespostas);
    console.log(respostas);
    if (perguntaAtual < perguntas.length - 1)
      setPerguntaAtual(perguntaAtual + 1);
  }
  function alterPerguntaGabarito(id) {
    setPerguntaGabarito(perguntas.find((item) => item.id == id));
  }
  const numColumns = 10;
  return (
    <Container>
      <Header />
      {perguntaAtual != undefined && !gabaritoVisivel && (
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
          {perguntaAtual <= perguntas.length - 1 && (
            <Pergunta
              data={perguntas[perguntaAtual]}
              perguntaAtual={perguntaAtual + 1}
              responder={responderPergunta.bind(this)}
              resposta={respostas[perguntas[perguntaAtual].id.toString()]}
            />
          )}
        </React.Fragment>
      )}
      {perguntaAtual == perguntas.length - 1 && !gabaritoVisivel && (
        <Button
          title="Enviar Respostas"
          onPress={() => {
            setGabaritoVisivel(true);
          }}
        />
      )}
      {gabaritoVisivel && (
        <Gabarito
          perguntas={perguntas}
          respostas={respostas}
          detalharPergunta={alterPerguntaGabarito.bind(this)}
        />
      )}

      {perguntaGabarito && (
        // <View>
        //   <Text>{perguntaGabarito.text}</Text>
        //   <Text>
        //     {
        //       perguntaGabarito.answers.find(
        //         (item) => item.id == perguntaGabarito.correct
        //       ).text
        //     }
        //   </Text>
        //   <Text>
        //     {
        //       perguntaGabarito.answers.find(
        //         (item) => item.id == respostas[perguntaGabarito.id.toString()]
        //       ).text
        //     }
        //   </Text>
        // </View>
        <Pergunta
          data={perguntaGabarito}
          responder={() => {}}
          readOnly
          resposta={respostas[perguntaGabarito.id.toString()]}
        />
      )}
    </Container>
  );
}
