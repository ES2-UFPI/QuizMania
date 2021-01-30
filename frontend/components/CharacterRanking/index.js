import React from 'react'
import { ScrollView, StyleSheet, View, Text} from 'react-native'


export default function ResponsiveList({porcentagem, nome}) {
  return(
    <View style={{...styles.container, height: porcentagem.toString() + '%'}}>
      <Text>{nome}</Text>
      <View style={{backgroundColor: 'black'}}></View>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    width: 40
  },
})