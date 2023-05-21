/* eslint-disable import/no-extraneous-dependencies */
import React, { useEffect, useState } from 'react';
import { Button, message } from 'antd';
import ModalCreateAndUpdateParking from '../Components/Modal/ModalCreateAndUpdateParking';
import TableListParking from '../Components/Table/TableParking';
import { api } from '../Services/HttpHandler';
import ModalCreateAndUpdatePrice from '../Components/Modal/ModalCreateAndUpdatePrice';
import TableListPrice from '../Components/Table/TablePrice';

export default function Home() {
  const [openModalPrice, setOpenModalPrice] = useState(false);
  const [openModalParking, setOpenModalParking] = useState(false);
  const [dataSource, setDataSource] = useState([]);
  const [dataSourcePrice, setDataSourcePrice] = useState([]);
  const [updateTable, setUpdateTable] = useState(false);

  const showModalParking = (createParking) => {
    if (createParking) {
      setOpenModalParking(true);
    } else {
      setOpenModalParking(false);
    }
  };

  const showModalPrice = () => {
    setOpenModalPrice(true);
  };

  const getAllParking = async () => {
    await api
      .get('api/Parking')
      .then(({ data }) => {
        setDataSource(data);
      })
      .catch(() => {
        message.error('A tabela esta vazia!');
      });
  };

  const getAllPrice = async () => {
    await api
      .get('api/Price')
      .then(({ data }) => {
        if (!data.lenght === 0) {
          showModalPrice();
        }
        setDataSourcePrice(data);
      })
      .catch(() => {
        message.error('A tabela esta vazia!');
      });
  };

  useEffect(() => {
    getAllParking();
    getAllPrice();
  }, [updateTable]);

  return (
    <div
      style={{
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
      }}
    >
      <div className="Content" style={{ width: '80%' }}>
        <Button type="primary" onClick={() => showModalParking(true)}>
          Registrar entrada
        </Button>

        <Button
          type="primary"
          style={{ marginLeft: 5 }}
          onClick={() => showModalPrice()}
        >
          Registrar Valores do Estacionamento
        </Button>

        <ModalCreateAndUpdateParking
          open={openModalParking}
          setOpenModal={setOpenModalParking}
          UpdateTable={updateTable}
          setUpdateTable={setUpdateTable}
        />

        <ModalCreateAndUpdatePrice
          open={openModalPrice}
          setOpenModal={setOpenModalPrice}
          UpdateTable={updateTable}
          setUpdateTable={setUpdateTable}
        />

        <div
          style={{
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'space-evenly',
          }}
        >
          <TableListPrice
            contentList={dataSourcePrice}
            UpdateTable={updateTable}
            setUpdateTable={setUpdateTable}
          />

          <TableListParking
            contentList={dataSource}
            UpdateTable={updateTable}
            setUpdateTable={setUpdateTable}
          />
        </div>
      </div>
    </div>
  );
}
