/* eslint-disable react/no-unstable-nested-components */
/* eslint-disable react/prop-types */
import { Table } from 'antd';
import React, { useState } from 'react';

import ModalCreateAndUpdateParking from '../Modal/Modal';

export default function TableListParkin({ contentList }) {
  const [openUpdateModal, setOpenUpdateModal] = useState(false);
  const [UpdateParking, setUpdateParking] = useState();

  const getAllParking = () => {
    const listObjects = [];
    contentList?.forEach((element) => {
      console.log(element);
      listObjects.push({
        key: element.id,
        licenseplate: element?.licensePlate,
        entryDate: element?.entryDate,
        departureDate: element?.departureDate,
        lenghOfStay: element?.lenghOfStay,
        chargedTime: element?.chargedTime,
        price: element?.price,
        amountToPay: element?.amountCharged,
      });
    });
    return listObjects;
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
      dataIndex: 'price',
      key: 'price',
    },
    {
      title: 'AmountToPay',
      dataIndex: 'amountToPay',
      key: 'amountToPay',
    },
  ];

  return (
    <>
      <Table
        pagination={false}
        onRow={(e) => ({
          onDoubleClick: async () => {
            setUpdateParking(e.key);
            setOpenUpdateModal(true);
          },
        })}
        dataSource={getAllParking()}
        columns={columns}
      />
      <ModalCreateAndUpdateParking
        open={openUpdateModal}
        setOpenModal={setOpenUpdateModal}
        UpdateIdParking={UpdateParking}
      />
    </>
  );
}
