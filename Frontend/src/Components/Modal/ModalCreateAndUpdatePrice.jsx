/* eslint-disable no-unused-vars */
/* eslint-disable react/prop-types */
/* eslint-disable import/no-extraneous-dependencies */
import React, { useEffect, useState } from 'react';
import {
  Modal,
  Form,
  Row,
  Input,
  DatePicker,
  Button,
  Col,
  InputNumber,
  message,
} from 'antd';
import dayjs from 'dayjs';
import locale from 'antd/es/date-picker/locale/pt_BR';
import { api } from '../../Services/HttpHandler';

export default function ModalCreateAndUpdatePrice({
  open,
  setOpenModal,
  setUpdateTable,
  udpdateTable,
  UpdatePrice = false,
}) {
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();

  const onFinish = async (values) => {
    setLoading(true);
    const objectCreateParking = {
      initialDate: values.initialDate.subtract(3, 'hours'),
      finalDate: values.finalDate.subtract(3, 'hours'),
      initialTime: values.initialTime,
      initialTimeValue: values.initialTimeValue,
      additionalHourlyValue: values.additionalHourlyValue,
    };
    await api
      .post('api/Price', objectCreateParking)
      .then(({ data }) => {
        form.setFieldsValue({
          ...data,
          initialDate: data?.initialDate ? dayjs(data?.initialDate) : '',
          finalDate: data?.finalDate ? dayjs(data?.finalDate) : '',
        });
        setLoading(false);
        setOpenModal(false);
        form.resetFields();
        setUpdateTable(!udpdateTable);
        message.success('Tabela de preço criada com sucesso!');
      })
      .catch(({ response }) => {
        message.error(response?.data?.Message);
        setLoading(false);
      });
  };

  form.setFieldsValue(UpdatePrice);

  const onUpdatePrice = async () => {
    setLoading(true);
    const values = form.getFieldsValue();
    const objectCreatePrice = {
      id: 1,
      initialDate: values.initialDate.subtract(3, 'hours'),
      finalDate: values.finalDate.subtract(3, 'hours'),
      initialTime: values.initialTime,
      initialTimeValue: values.initialTimeValue,
      additionalHourlyValue: values.additionalHourlyValue,
    };
    await api
      .put('api/Price', objectCreatePrice)
      .then(({ data }) => {
        form.setFieldsValue({
          ...data,
          initialDate: data?.initialDate ? dayjs(data?.initialDate) : '',
          finalDate: data?.finalDate ? dayjs(data?.finalDate) : '',
        });
        message.success('Tabela de preço atualizada com sucesso!');
        setLoading(false);
        form.resetFields();
        setOpenModal(false);
        setUpdateTable(!udpdateTable);
      })
      .catch(({ response }) => {
        message.error(response?.data?.Message);
        setLoading(false);
      });
  };

  return (
    <Modal
      className="simple-modal"
      open={open}
      footer={false}
      onCancel={() => setOpenModal(false)}
      closable={false}
    >
      <Form
        form={form}
        disabled={loading}
        onFinish={UpdatePrice ? onUpdatePrice : onFinish}
        layout="vertical"
      >
        <Row gutter={[24]}>
          <Col span={24}>
            <Form.Item
              label="Data de inicio vigência"
              name="initialDate"
              rules={[
                {
                  required: true,
                  message: 'Informar a Data de inicio vigência é obrigatório!',
                },
              ]}
            >
              <DatePicker
                locale={locale}
                showTime
                style={{ width: '100%' }}
                format="DD/MM/YYYY HH:mm:ss"
              />
            </Form.Item>
          </Col>

          <Col span={24}>
            <Form.Item
              label="Data final vigência"
              name="finalDate"
              rules={[
                {
                  required: true,
                  message: 'Informar a Data final vigência é obrigatório!',
                },
              ]}
            >
              <DatePicker
                locale={locale}
                showTime
                style={{ width: '100%' }}
                format="DD/MM/YYYY HH:mm:ss"
              />
            </Form.Item>
          </Col>

          <Col span={24}>
            <Form.Item
              initialValue={60}
              label="Hora Inicial (em minutos)"
              name="initialTime"
              rules={[
                {
                  required: true,
                  message: 'Informar a Hora Inicial é obrigatório!',
                },
              ]}
            >
              <InputNumber disabled style={{ width: '100%' }} />
            </Form.Item>
          </Col>

          <Col span={24}>
            <Form.Item
              label="Valor da hora Inicial"
              name="initialTimeValue"
              rules={[
                {
                  required: true,
                  message: 'Informar a Hora Inicial é obrigatório!',
                },
              ]}
            >
              <InputNumber style={{ width: '100%' }} />
            </Form.Item>
          </Col>

          <Col span={24}>
            <Form.Item
              label="Valor da hora adicional"
              name="additionalHourlyValue"
              rules={[
                {
                  required: true,
                  message: 'Informar o Valor da hora adicional é obrigatório!',
                },
              ]}
            >
              <InputNumber style={{ width: '100%' }} />
            </Form.Item>
          </Col>
        </Row>

        <Button type="primary" htmlType="submit">
          Enviar
        </Button>
      </Form>
    </Modal>
  );
}
