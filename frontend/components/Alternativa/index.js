import React, { useEffect, useRef, useState } from "react";
import { Button } from "react-native-elements";
import { StyleSheet, Animated, TouchableOpacity } from "react-native";

export default function Alternativa({
  data,
  perguntaAtual,
  responder,
  index,
  selected,
  readOnly,
  correct,
  renderNovaAlternativaSelecionada,
}) {
  const translateValue = useRef(new Animated.Value(500)).current;
  const [pressed, setPressed] = useState(undefined);
  const fadeIn = () => {
    // Will change fadeAnim value to 1 in 5 seconds
    translateValue.setValue(500 * (index + 1));
    Animated.spring(translateValue, {
      toValue: 0,
      delay: index * 500,
      useNativeDriver: true,
    }).start();
  };

  useEffect(() => {
    fadeIn();
  }, [data]);

  return (
    <Animated.View style={{ transform: [{ translateY: translateValue }] }}>
      <Button
        title={data.text}
        buttonStyle={[
          styles.button,
          readOnly
            ? correct
              ? { backgroundColor: "green" }
              : selected
              ? { backgroundColor: "red" }
              : { backgroundColor: "gray" }
            : selected
            ? { backgroundColor: "green" }
            : {},
        ]}
        onPress={() => {
          renderNovaAlternativaSelecionada(data.id);
        }}
      />
    </Animated.View>
  );
}

const styles = StyleSheet.create({
  button: {
    marginVertical: 15,
  },
});
