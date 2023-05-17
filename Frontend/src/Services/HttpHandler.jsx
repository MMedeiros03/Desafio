/* eslint-disable import/prefer-default-export */
/* eslint-disable import/no-extraneous-dependencies */
import axios from 'axios';

export const api = axios.create({
  baseURL: 'https://localhost:7222/',
  headers: {
    'Content-Type': 'application/json charset=utf-8',
    'Cache-Control': 'max-age=31536000',
  },
  maxBodyLength: Infinity,
});
