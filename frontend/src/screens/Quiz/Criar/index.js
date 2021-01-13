import React, { useState, useEffect } from "react";
import { Text, View, FlatList, TouchableOpacity, Picker, Switch } from "react-native";
import { TextInput } from "react-native-paper";
import { Button, Card } from "react-native-elements";
import { Container, Header, Pergunta, Gabarito } from "../../../../components";
import API from "../../../../services";

function PerguntaItem(data) {
  return (
    <View>
      <Text>data.titulo</Text>
      {data.respostas.map((item, index) => (
        <View>{item.text}</View>
      ))}
    </View>
  );
}

function AlternativaItem({data, modifyData, index}) {
  return (
    <View style={{flexDirection: 'row', marginVertical: 15, justifyContent: 'space-between'}}>
      <Text style={{color: 'black'}}>{data.text}</Text>
      <View style={{flexDirection: 'row'}}>
        <Text>Correta? </Text>
        <Switch value={data.correct} onValueChange={(value) => modifyData({...data, correct: value}, index)  }/>
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

  async function submit() {}


  function addAlternativa() {
    alert("Alterando.." + perguntaAlternativa)
    console.log(alternativas)
    alternativas.push({
      "text": perguntaAlternativa,
      "correct": correta,
    })
    setAlternativas(alternativas)
  }

  function modifyData(data, index) {
    // const alternativas_temp = alternativas
    // alternativas_temp[index] = data
    alternativas[index] = data
    setAlternativas(alternativas)
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
          renderItem={({ item, index }) => {
            <PerguntaItem data={item} />;
          }}
        />

        <Button
          title="Criar pergunta"
          onPress={() => {
            setStep(1);
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
      <View style={{ flexDirection: "row", justifyContent: 'space-between', alignItems: 'center', marginVertical: 15 }}>
        <TextInput
          value={perguntaAlternativa}
          label="Alternativa"
          style={{ backgroundColor: "transparent", flex: 1 }}
          placeholder="Digite o título da alternativa"
          onChangeText={(texto) => setPerguntaAlternativa(texto)}
        />
        <View>
          <Text>Correta?</Text>
          <Switch value={correta} onValueChange={(value) => setCorreta(value)}/>
        </View>
        <Button type="clear" title="Adicionar" onPress={() => addAlternativa()}/>
      </View>
      
      <FlatList
        data={alternativas}
        renderItem={({ item, index }) => {
          return <AlternativaItem data={item} index={index} modifyData={modifyData.bind(this)} />;
        }}
      />
      <Button
        title="Voltar"
        onPress={() => {
          setStep(0);
        }}
      />
      
    </Container>
  );
}
