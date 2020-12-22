import axios from 'axios'


class ApiConnection {
  constructor() {
    this.api = axios.create({
      baseURL: "https://quizmaniaapplication.azurewebsites.net/"
    })
  }

  async obterPersonagem({ id=1 }) {
    try {
      const response = await this.api.get(`character/${id}`);
      return response.data;
    } catch (error) {
      alert(error.toString()) 
    }
  }


  async obterQuizzes() {
    try {
      const response = await this.api.get(`quiz`);
      return response.data;
    } catch (error) {
      alert(error.toString()) 
    }
  }

  async detalharQuiz(id) {
    try {
      const response = await this.api.get(`quiz/${id}`);
      return response.data;
    } catch (error) {
      alert(error.toString()) 
    }
  }

  async responderQuiz(data) {
    try {
      const response = await this.api.post(`quiz/`, data);
      return response.data;
    } catch (error) {
      alert(error.toString()) 
    }
  }

}

export default new ApiConnection();