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
          ? dayjs(element?.entryDate).format('DD-MM-YYYY HH:mm:ss')
          : '',
        departureDate: element?.departureDate
          ? dayjs(element?.departureDate).format('DD-MM-YYYY HH:mm:ss')
          : '',
        lenghOfStay: element?.lenghOfStay,
        chargedTime: element?.chargedTime,
        priceCharged: element?.priceCharged,
        amountToPay: element?.amountCharged,
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
      title: 'LicensePlate',
      dataIndex: 'licenseplate',
      key: 'licensePlate',
    },
    {
      title: 'EntryDate',
      dataIndex: 'entryDate',
      key: 'entryDate',
    },
    {
      title: 'DepartureDate',
      dataIndex: 'departureDate',
      key: 'departureDate',
    },
    {
      title: 'LenghOfStay',
      dataIndex: 'lenghOfStay',
      key: 'lenghOfStay',
    },
    {
      title: 'ChargedTime',
      dataIndex: 'chargedTime',
      key: 'chargedTime',
    },
    {
      title: 'Price',
      dataIndex: 'priceCharged',
      key: 'priceCharged',
    },
    {
      title: 'AmountToPay',
      dataIndex: 'amountToPay',
      key: 'amountToPay',
    },
  ];

  useEffect(() => {
    getAllParking();
  }, []);

  return (
    <>
      <Table
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
