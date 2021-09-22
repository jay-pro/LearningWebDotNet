import React, { useState } from "react";
import { Table } from "antd";

function TableUser({ columns, data, loading }) {
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(3);

  return (
    <div>
      <h1 className="table-User-title">Danh sách các users</h1>
      <div className="table-User">
        <Table
          columns={columns}
          dataSource={data}
          pagination={{
            current: page,
            pageSize: pageSize,
            onChange: (page, pageSize) => {
              setPage(page);
              setPageSize(pageSize);
            },
          }}
          loading={loading}
          rowKey="idUser"
        />
      </div>
    </div>
  );
}

export default TableUser;
