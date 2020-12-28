import React, { useState, useEffect } from "react";
import { Text, View, FlatList } from "react-native";
import { Button } from "react-native-elements";
import { Container, Header, Pergunta, Gabarito } from "../../../../components";
import { useStyled } from "react-native-reflect";
import API from '../../../../services'


export default function responderQuiz({ navigation, route }) {
  const [quiz, setQuiz] = useState(route.params.quiz);
  const [perguntas, setPerguntas] = useState(route.params.quiz.questions || []);
  const [perguntaAtual, setPerguntaAtual] = useState(undefined);
  const [respostas, setRespostas] = useState({});
  const [gabaritoVisivel, setGabaritoVisivel] = useState(false);
  const [perguntaGabarito, setPerguntaGabarito] = useState(undefined);
  const [perguntasGabarito, setPerguntasGabarito] = useState(undefined);

  useEffect(() => {
    const focusListener = navigation.addListener("focus", () => {
      // Call ur function here.. or add logic.
      setQuiz(route.params.quiz);
      setPerguntas(route.params.quiz.questions);
      setRespostas({});
      setGabaritoVisivel(false);
      setPerguntaGabarito(undefined);
      setPerguntaAtual(0);
    });

    // focusListener()
    return focusListener;
  }, [navigation]);

  async function submitRespostas() {
    const respostasToSubmit = {}
    respostasToSubmit['questionAnswers'] = []
    Object.keys(respostas).forEach(function (key) {
      const respostaTemp = {}
      respostaTemp['questionId'] = key
      respostaTemp['answerIds'] = respostas[key]
      respostasToSubmit.questionAnswers.push(respostaTemp)
      console.log(key + ": " + respostas[key]);
    });
    respostasToSubmit['quizId'] = quiz.id
    console.log(respostasToSubmit)

    const data = await API.responderQuiz(respostasToSubmit)
    console.log(data)

    setPerguntasGabarito(data.questionAnswers)
    setGabaritoVisivel(true)
  }

  function responderPergunta(pergunta, resposta) {
    const novasRespostas = respostas;
    novasRespostas[pergunta] = resposta;
    setRespostas(novasRespostas);
    // console.log(respostas);
    if (perguntaAtual < perguntas.length - 1)
      setPerguntaAtual(perguntaAtual + 1);
  }
  function alterPerguntaGabarito(id) {
    console.log(perguntasGabarito)
    setPerguntaGabarito(perguntasGabarito.find((item) => item.question.id == id));
  }



  function isCorrect(alternativa, pergunta) {
    console.log(alternativa)
    console.log(pergunta)
    const contexto = pergunta
    const idPergunta = pergunta.question ? pergunta.question.id : pergunta.id
    const correct = contexto.answersId ? contexto.answersId.includes(alternativa.id) : false
    console.log(correct)
    return correct
    
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
              isCorrect={isCorrect.bind(this)}
              responder={responderPergunta.bind(this)}
              resposta={respostas[perguntas[perguntaAtual].id.toString()]}
              setGabaritoVisivel={setGabaritoVisivel.bind(this)}
              navigation={navigation}
              submitRespostas={submitRespostas.bind(this)}
              proximaPergunta={
                !(perguntaAtual == perguntas.length - 1 && !gabaritoVisivel)
              }
            />
          )}
        </React.Fragment>
      )}
      {gabaritoVisivel && (
        <Gabarito
          perguntas={perguntasGabarito}
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
          resposta={respostas[perguntaGabarito.question.id]}
          isCorrect={isCorrect.bind(this)}
        />
      )}
    </Container>
  );
}
