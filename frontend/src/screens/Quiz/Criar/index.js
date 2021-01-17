import React, { useState, useEffect } from "react";
import {
  Text,
  View,
  FlatList,
  TouchableOpacity,
  Picker,
  Switch,
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
          color="red"
          onPress={() => {
            if (!data.id) {
              deletePergunta(index);
            }
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
          value={data.correct}
          onValueChange={(value) =>
            readOnly
              ? undefined
              : modifyData({ ...data, correct: value }, index)
          }
        />
      </View>
    </View>
  );
}

export default function ListarQuizzes({ navigation }) {
  const [perguntas, setPerguntas] = useState([]);
  const [alternativas, setAlternativas] = useState([]);
  const [perguntaTitulo, setPerguntaTitulo] = useState("");
  const [perguntaAlternativa, setPerguntaAlternativa] = useState("");
  const [titulo, setTitulo] = useState("");
  const [correta, setCorreta] = useState("");
  const [step, setStep] = useState(0);
  const [tick, setTick] = useState(0);

  useEffect(() => {
    getData();
  }, []);

  async function getData() {
    // try {
    //   const data = await API.obterQuizzes();
    //   setQuizzes(data);
    // } catch (error) {
    //   alert(error)
    // }
  }

  async function submit() {
    console.log(perguntas)
    const request = {}
    request['questions'] = perguntas
    try {
      const response = await API.criarQuiz(request)
      alert(response.result)
    } catch (error) {
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

  function deletePergunta(index) {
    delete perguntas[index];
    setPerguntas(perguntas);
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

        <Button
          title="Cadastrar Quiz"
          containerStyle={{marginTop: 10}}
          onPress={() => {
            submit()
          }}
        />
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

      <Button
        title="Salvar"
        containerStyle={{ marginTop: 15 }}
        onPress={() => {
          const novaPergunta = {
            text: perguntaTitulo,
            answers: alternativas,
          };
          perguntas.push(novaPergunta);
          setPerguntas(perguntas);
          console.log(perguntas);
          setAlternativas([]);

          setPerguntaTitulo("");
          setPerguntaAlternativa("");
          setCorreta(false);
          setStep(0);
        }}
      />
    </Container>
  );
}
