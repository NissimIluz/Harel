import axios from 'axios';
import { CustomerDto } from '../types/models';

const BASE_URL = 'https://localhost:7178/api'; // או בהתאם לכתובת ה-API

export const login = async (username: string, password: string) => {
  return axios.post(`${BASE_URL}/auth/login`, { username, password });
};

export const fetchCustomers = () => axios.get(`${BASE_URL}/customers`);
export const getCustomerById = (id: number) => axios.get(`${BASE_URL}/customers/${id}`);
export const createCustomer = (data: CustomerDto) => axios.post(`${BASE_URL}/customers`, data);
export const updateCustomer = (id: number, data: CustomerDto) =>
  axios.put(`${BASE_URL}/customers/${id}`, data);
export const deleteCustomer = (id: number) =>
  axios.delete(`${BASE_URL}/customers/${id}`);
