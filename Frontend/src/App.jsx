import React from 'react';
import { ConfigProvider, theme } from 'antd';
import ptBR from 'antd/es/locale/pt_BR';
import Home from './Pages/Home';
import './App.css';

function App() {
  return (
    <ConfigProvider
      locale={ptBR}
      theme={{
        algorithm: theme.defaultAlgorithm,
        token: {
          colorPrimary: '#05b9f5',
          borderRadius: 16,
        },
      }}
    >
      <Home />
    </ConfigProvider>
  );
}

export default App;
