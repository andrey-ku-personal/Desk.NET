import axios from "axios";

export const BaseHttp = axios.create({
  baseURL: `http://localhost:56895/api/`,
});
