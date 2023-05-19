/* eslint-disable react/no-unstable-nested-components */
/* eslint-disable react/prop-types */
import { Table, message } from 'antd';
import React, { useState, useEffect } from 'react';
import dayjs from 'dayjs';
import { api } from '../../Services/HttpHandler';
import ModalCreateAndUpdatePrice from '../Modal/ModalCreateAndUpdatePrice';

export default function TableListPrice({
  contentList,
  setUpdateTable,
  UpdateTable,
}) {
  const [openUpdateModal, setOpenUpdateModal] = useState(false);
  const [UpdatePrice, setUpdatePrice] = useState();

  const getAllPrice = () => {
    const listObjects = [];
    contentList?.forEach((element) => {
      listObjects.push({
        key: element?.id,
        initialDate: element?.initialDate
          ? dayjs(element?.initialDate).format('DD/MM/YYYY HH:mm:ss')
          : '',
        finalDate: element?.finalDate
          ? dayjs(element?.finalDate).format('DD/MM/YYYY HH:mm:ss')
          : '',
        initialTime: element?.initialTime,
        initialTimeValue: element?.initialTimeValue,
        additionalHourlyValue: element?.additionalHourlyValue,
      });
    });
    return listObjects;
  };

  const getPriceById = async (id) => {
    await api
      .get(`api/Price?id=${id}`)
      .then(({ data }) => {
        setUpdatePrice({
          ...data,
          initialDate: data?.initialDate ? dayjs(data?.initialDate) : '',
          finalDate: data?.finalDate ? dayjs(data?.finalDate) : '',
        });
      })
      .catch(() => {
        message.error('Houve um erro!');
      });
  };

  const columns = [
    {
      title: 'Data Inicial vigencia',
      dataIndex: 'initialDate',
      key: 'initialDate',
      align: 'center',
    },
    {
      title: 'Data Final vigencia',
      dataIndex: 'finalDate',
      key: 'finalDate',
      align: 'center',
    },
    {
      title: 'Hora Inicial',
      dataIndex: 'initialTime',
      key: 'initialTime',
      align: 'center',
    },
    {
      title: 'Valor da Hora Inicial',
      dataIndex: 'initialTimeValue',
      key: 'lenghOfStay',
      align: 'center',
    },
    {
      title: 'Valor Adicional da Hora',
      dataIndex: 'additionalHourlyValue',
      key: 'additionalHourlyValue',
      align: 'center',
    },
  ];

  useEffect(() => {
    getAllPrice();
  }, []);

  return (
    <>
      <Table
        style={{ marginBottom: 50 }}
        scroll={{ y: 300 }}
        pagination={false}
        onRow={(e) => ({
          onDoubleClick: async () => {
            getPriceById(e.key);
            setOpenUpdateModal(true);
          },
        })}
        dataSource={getAllPrice()}
        columns={columns}
      />
      <ModalCreateAndUpdatePrice
        open={openUpdateModal}
        setOpenModal={setOpenUpdateModal}
        UpdatePrice={UpdatePrice}
        setUpdateTable={setUpdateTable}
        UpdateTable={UpdateTable}
      />
    </>
  );
}
