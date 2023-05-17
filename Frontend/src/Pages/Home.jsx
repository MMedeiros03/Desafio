/* eslint-disable import/no-extraneous-dependencies */
import React, { useEffect, useState } from 'react';
import { Button } from 'antd';
import ModalCreateAndUpdateParking from '../Components/Modal/Modal';
import TableListParkin from '../Components/Table/Table';
import { api } from '../Services/HttpHandler';

export default function Home() {
  const [openModal, setOpenModal] = useState(false);
  const [openModalCreate, setOpenModalCreate] = useState();
  const [dataSource, setDataSource] = useState([]);

  const showModal = (createParking) => {
    if (createParking) {
      setOpenModalCreate(true);
    } else {
      setOpenModalCreate(false);
    }
    setOpenModal(true);
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
  }, []);

  return (
    <div className="ContentMain">
      <Button type="primary" onClick={() => showModal(true)}>
        Registrar entrada
      </Button>

      <ModalCreateAndUpdateParking
        modalCreate={openModalCreate}
        open={openModal}
        setOpenModal={setOpenModal}
      />
      <TableListParkin contentList={dataSource} />
    </div>
  );
}
