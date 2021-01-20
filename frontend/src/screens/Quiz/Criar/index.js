import React, { useState, useEffect } from "react";
import {
  Text,
  View,
  FlatList,
  TouchableOpacity,
  Picker,
  Switch,
  Alert
} from "react-native";
import { TextInput } from "react-native-paper";
import { Button, Card } from "react-native-elements";
import { Container, Header, Pergunta, Gabarito } from "../../../../components";
import API from "../../../../services";
import { Ionicons } from "@expo/vector-icons";

function PerguntaItem({ data, index, deletePergunta }) {
  if (!data) return <View></View>;
  return (
    <Card>
      <View
        style={{
          flexDirection: "row",
          justifyContent: "space-between",
          marginBottom: 20,
        }}
      >
        <Text>{data.text}</Text>
        <Ionicons
          name="md-trash"
          size={24}
          style={{right: 30}}
          color="red"
          onPress={() => {
            Alert.alert(
              "Atenção",
              "Deseja realmente deletar a pergunta?",
              [
                {
                  text: "Cancelar",
                  onPress: () => console.log("Cancel Pressed"),
                  style: "cancel"
                },
                { text: "OK", onPress: () => deletePergunta(index) }
              ],
            );
          }}
        />
      </View>
      {data.answers.map((item, index) => (
        <AlternativaItem data={item} index={index} readOnly />
      ))}
    </Card>
  );
}

function AlternativaItem({ data, modifyData, index, readOnly }) {
  return (
    <View
      style={{
        flexDirection: "row",
        marginVertical: 15,
        justifyContent: "space-between",
      }}
    >
      <Text style={{ color: "black" }}>{data.text}</Text>
      <View style={{ flexDirection: "row" }}>
        <Text>Correta? </Text>
        <Switch
          value={data.isCorrect}
          onValueChange={(value) =>
            readOnly
              ? undefined
              : modifyData({ ...data, isCorrect: value }, index)
          }
        />
      </View>
    </View>
  );
}

export default function ListarQuizzes({ navigation, route }) {
  const [quiz, setQuiz] = useState(route.params?.quizId || undefined)
  const [perguntas, setPerguntas] = useState([]);
  const [alternativas, setAlternativas] = useState([]);
  const [perguntaTitulo, setPerguntaTitulo] = useState("");
  const [perguntaAlternativa, setPerguntaAlternativa] = useState("");
  const [titulo, setTitulo] = useState("");
  const [correta, setCorreta] = useState("");
  const [step, setStep] = useState(0);
  const [tick, setTick] = useState(0);
  const [character, setCharacter] = useState(0)

  useEffect(() => {
    if(quiz) {
      getData();
    }
  }, []);

  async function getData() {
    try {
      const data = await API.detalharQuiz(quiz);
      setCharacter(data.owner.id)
      setPerguntas(data.questions);
      setTitulo(data.title)
      console.log(perguntas)
    } catch (error) {
      alert(error.response.data.result)
    }
  }

  async function submit() {
    console.log(perguntas)
    const request = {}
    request['questions'] = perguntas.filter((item, index) => item != null)
    request['title'] = titulo
    try {
      console.log(JSON.stringify(request))
      const response = await API.criarQuiz(request)
      alert(response.result)
      navigation.goBack()
    } catch (error) {
      alert(error.response.result)
      console.log(error)
    }
  }

  function addAlternativa() {
    alternativas.push({
      text: perguntaAlternativa,
      isCorrect: correta,
    });
    setAlternativas(alternativas);
    setTick(tick + 1);
  }

  async function deletePergunta(index) {
    if(perguntas[index].id) {
      try {
        const body = {
          quizId: quiz,
          questionId: perguntas[index].id,
          characterId: character
        }
        const response = await API.deletarPergunta(body)
        alert("Pergunta deletada com sucesso!")
        await getData()
      } catch (error) {
        alert("Erro ao deletar pergunta")
        console.log(error)
      }

    } else {
      delete perguntas[index];
      setPerguntas(perguntas);
    }
    setTick(tick + 1);
  }

  function modifyData(data, index) {
    // const alternativas_temp = alternativas
    // alternativas_temp[index] = data
    alternativas[index] = data;
    setAlternativas(alternativas);
    setTick(tick + 1);
  }

  if (step == 0) {
    return (
      <Container navigation={navigation}>
        <TextInput
          value={titulo}
          label="Título"
          editable={!quiz}
          style={{ backgroundColor: "transparent" }}
          placeholder="Digite o título do Quiz"
          onChangeText={(texto) => setTitulo(texto)}
        />

        <FlatList
          data={perguntas}
          keyExtractor={(item, index) => "key" + index}
          renderItem={({ item, index }) => {
            return (
              <PerguntaItem
                data={item}
                index={index}
                deletePergunta={deletePergunta.bind(this)}
              />
            );
          }}
        />

        <Button
          title="Criar pergunta"
          containerStyle={{marginTop:20}}
          onPress={() => {
            setStep(1);
          }}
        />

        {(!quiz && perguntas.length > 0 && titulo.length > 0) && (
          <Button
          title="Cadastrar Quiz"
          containerStyle={{marginTop: 10}}
          onPress={() => {
            submit()
          }}
        />
        )}
      </Container>
    );
  }

  return (
    <Container navigation={navigation}>
      <TextInput
        value={perguntaTitulo}
        label="Título da pergunta"
        style={{ backgroundColor: "transparent" }}
        placeholder="Digite o título da Pergunta"
        onChangeText={(texto) => setPerguntaTitulo(texto)}
      />
      <View
        style={{
          flexDirection: "row",
          justifyContent: "space-between",
          alignItems: "center",
          marginVertical: 15,
        }}
      >
        <TextInput
          value={perguntaAlternativa}
          label="Alternativa"
          style={{ backgroundColor: "transparent", flex: 1 }}
          placeholder="Digite o título da alternativa"
          onChangeText={(texto) => setPerguntaAlternativa(texto)}
        />
        <View>
          <Text>Correta?</Text>
          <Switch
            value={correta}
            onValueChange={(value) => setCorreta(value)}
          />
        </View>
        <Button
          type="clear"
          title="Adicionar"
          onPress={() => addAlternativa()}
        />
      </View>

      <FlatList
        data={alternativas}
        renderItem={({ item, index }) => {
          return (
            <AlternativaItem
              data={item}
              index={index}
              modifyData={modifyData.bind(this)}
            />
          );
        }}
      />
      <Button
        title="Cancelar"
        onPress={() => {
          setStep(0);
        }}
      />

     {alternativas.length > 1 && alternativas.find((item, index) => item.isCorrect) && <Button
        title="Salvar"
        containerStyle={{ marginTop: 15 }}
        onPress={async () => {
          const novaPergunta = {
            text: perguntaTitulo,
            answers: alternativas,
          };
          if(quiz) {
            novaPergunta['quizId'] = quiz
            try {
              console.log(JSON.stringify(novaPergunta))
              const response = await API.criarPergunta(novaPergunta)
              alert("Sua pergunta foi cadastrada com sucesso ao quiz!")
              await getData() 
            } catch (error) {
              alert("Erro ao cadastrar sua pergunta...")
              console.log(error)
            }
            
          } else {

            perguntas.push(novaPergunta);
            setPerguntas(perguntas);
            console.log(perguntas);
            setAlternativas([]);
            
          }
          setPerguntaTitulo("");
          setPerguntaAlternativa("");
          setCorreta(false);
          setStep(0);
        }}
      />}
    </Container>
  );
}
