/* eslint-disable react/no-unstable-nested-components */
/* eslint-disable react/prop-types */
import { Table } from 'antd';
import React, { useState, useEffect } from 'react';
import dayjs from 'dayjs';
import { api } from '../../Services/HttpHandler';
import ModalCreateAndUpdateParking from '../Modal/ModalCreateAndUpdateParking';

export default function TableListParking({
  contentList,
  setUpdateTable,
  UpdateTable,
}) {
  const [openUpdateModal, setOpenUpdateModal] = useState(false);
  const [UpdateParking, setUpdateParking] = useState();

  const getAllParking = () => {
    const listObjects = [];
    contentList?.forEach((element) => {
      listObjects.push({
        key: element?.id,
        licenseplate: element?.licensePlate,
        entryDate: element?.entryDate
          ? dayjs(element?.entryDate).format('DD/MM/YYYY HH:mm:ss')
          : '',
        departureDate: element?.departureDate
          ? dayjs(element?.departureDate).format('DD/MM/YYYY HH:mm:ss')
          : '',
        lenghOfStay: element?.lenghOfStay,
        chargedTime: element?.chargedTime
          ? `${element?.chargedTime} horas`
          : '',
        priceCharged: element?.priceCharged
          ? `R$ ${element?.priceCharged}`
          : '',
        amountToPay: element?.amountCharged
          ? `R$ ${element?.amountCharged}`
          : '',
      });
    });
    return listObjects;
  };

  const getParkingById = async (id) => {
    await api
      .get(`api/Parking?id=${id}`)
      .then(({ data }) => {
        setUpdateParking({
          ...data,
          entryDate: data?.entryDate ? dayjs(data?.entryDate) : '',
          departureDate: data?.departureDate ? dayjs(data?.departureDate) : '',
        });
      })
      .catch((e) => {
        console.log(e);
      });
  };

  const columns = [
    {
      title: 'Placa',
      dataIndex: 'licenseplate',
      key: 'licensePlate',
      align: 'center',
    },
    {
      title: 'Data de Entrada',
      dataIndex: 'entryDate',
      key: 'entryDate',
      align: 'center',
    },
    {
      title: 'Data de Saída',
      dataIndex: 'departureDate',
      key: 'departureDate',
      align: 'center',
    },
    {
      title: 'Duração',
      dataIndex: 'lenghOfStay',
      key: 'lenghOfStay',
      align: 'center',
    },
    {
      title: 'Tempo Cobrado',
      dataIndex: 'chargedTime',
      key: 'chargedTime',
      align: 'center',
    },
    {
      title: 'Preço',
      dataIndex: 'priceCharged',
      key: 'priceCharged',
      align: 'center',
    },
    {
      title: 'Valor a Pagar',
      dataIndex: 'amountToPay',
      key: 'amountToPay',
      align: 'center',
    },
  ];

  useEffect(() => {
    getAllParking();
  }, []);

  return (
    <>
      <Table
        scroll={{ y: 300 }}
        pagination={false}
        onRow={(e) => ({
          onDoubleClick: async () => {
            getParkingById(e.key);
            setOpenUpdateModal(true);
          },
        })}
        dataSource={getAllParking()}
        columns={columns}
      />
      <ModalCreateAndUpdateParking
        open={openUpdateModal}
        setOpenModal={setOpenUpdateModal}
        UpdateParking={UpdateParking}
        setUpdateTable={setUpdateTable}
        UpdateTable={UpdateTable}
      />
    </>
  );
}
