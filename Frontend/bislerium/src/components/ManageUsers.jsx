import React from "react";

const ManageUsers = () => {
  return (
    <div>
      <p className="text-3xl">Manage Users</p>
      <br />
      <div className=" flex justify-between p-10 rounded-xl bg-red-200 items-center">
        <div className="text-lg font-semibold">
            UserName
            {/* render this dynamically */}
        </div>
        <div className="flex justify-between gap-2">
          <button className="bg-blue-400 p-2 rounded-lg">
            Change UserName
          </button>
          
          <button className="bg-blue-400 p-2 rounded-lg">
            Ban User
          </button>
        </div>
      </div>
    </div>
  );
};

export default ManageUsers;
