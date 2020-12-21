import React, { useState, useEffect } from "react";
import { Text, View, FlatList } from "react-native";
import { Button } from "react-native-elements";
import { Container, Header, Pergunta, Gabarito } from "../../../components";
import { useStyled } from "react-native-reflect";
import { useFocusEffect } from "@react-navigation/native";
import { useIsFocused } from "@react-navigation/native";
export default function responderQuiz({ navigation, route }) {
  const [quiz, setQuiz] = useState(route.params.quiz);
  const [perguntas, setPerguntas] = useState(route.params.quiz.questions || []);
  const [perguntaAtual, setPerguntaAtual] = useState(undefined);
  const [respostas, setRespostas] = useState({});
  const [gabaritoVisivel, setGabaritoVisivel] = useState(false);
  const [perguntaGabarito, setPerguntaGabarito] = useState(undefined);

  // useEffect(() => {
  //   const isFocused = useIsFocused();

  //   if (isFocused) {
  //     if (route.params.notReload) {
  //       alert("roi");
  //       setGabaritoVisivel(true);
  //     } else {
  //       setQuiz(route.params.quiz);
  //       setPerguntas(route.params.quiz.questions);
  //       setRespostas({});
  //       setGabaritoVisivel(false);
  //       setPerguntaGabarito(undefined);
  //       setPerguntaAtual(0);
  //     }
  //   }
  // }, [navigation]);

  useFocusEffect(
    React.useCallback(() => {
      const unsubscribe = () => {
        alert(route.params.notReload)
        if (route.params.notReload) {
          console.log("not")

          setGabaritoVisivel(true);
        } else {
          alert(route.params.notReload)
          setQuiz(route.params.quiz);
          setPerguntas(route.params.quiz.questions);
          setRespostas({});
          setGabaritoVisivel(false);
          setPerguntaGabarito(undefined);
          setPerguntaAtual(0);
        }
      };

      return () => unsubscribe();
    }, [])
  );

  function responderPergunta(pergunta, resposta) {
    const novasRespostas = respostas;
    novasRespostas[pergunta] = resposta;
    setRespostas(novasRespostas);
    // console.log(respostas);
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
              setGabaritoVisivel={setGabaritoVisivel.bind(this)}
              navigation={navigation}
              proximaPergunta={
                !(perguntaAtual == perguntas.length - 1 && !gabaritoVisivel)
              }
            />
          )}
        </React.Fragment>
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
