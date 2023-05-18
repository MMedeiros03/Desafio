/* eslint-disable import/no-extraneous-dependencies */
import React, { useEffect, useState } from 'react';
import { Button } from 'antd';
import ModalCreateAndUpdateParking from '../Components/Modal/ModalCreateAndUpdateParking';
import TableListParking from '../Components/Table/Table';
import { api } from '../Services/HttpHandler';
import ModalCreateAndUpdatePrice from '../Components/Modal/ModalCreateAndUpdatePrice';

export default function Home() {
  const [openModalPrice, setOpenModalPrice] = useState(false);
  const [openModalParking, setOpenModalParking] = useState(false);
  const [dataSource, setDataSource] = useState([]);
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
      .catch((e) => {
        console.log(e);
      });
  };

  useEffect(() => {
    getAllParking();
  }, [updateTable]);

  return (
    <div className="ContentMain">
      <Button type="primary" onClick={() => showModalParking(true)}>
        Registrar entrada
      </Button>

      <Button type="primary" onClick={() => showModalPrice()}>
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
      />
      <TableListParking
        contentList={dataSource}
        UpdateTable={updateTable}
        setUpdateTable={setUpdateTable}
      />
    </div>
  );
}
