import React from 'react'
import { ScrollView, StyleSheet} from 'react-native'


export default function ResponsiveList({children}) {
  return(
    <ScrollView contentContainerStyle={styles.containerPerguntas}>
      {children}
    </ScrollView>
  )
}

const styles = StyleSheet.create({
  containerPerguntas: { width: "100%", flexDirection: "row", flexWrap: "wrap" },
})