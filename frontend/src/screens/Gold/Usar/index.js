import React from 'react'
import { View, Text, FlatList} from 'react-native'
import { Container, Header } from '../../../../components'


export default function Usar() {
  return (
    <Container>
      <Header />
      <View>
        <Text style={{fontSize: 30, fontWeight: 'bold'}}>Loja</Text>
      </View>
    </Container>
  )
}