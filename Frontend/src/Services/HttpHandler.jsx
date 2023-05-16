import axios from 'axios';

export const api = axios.create({
  baseURL: process.env.HOST,
  headers: {
    'Content-Type': 'application/json charset=utf-8',
    'Cache-Control': 'max-age=31536000',
  },
  maxBodyLength: Infinity,
});
