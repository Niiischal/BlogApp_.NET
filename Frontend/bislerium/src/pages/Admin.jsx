"use client";

import React, { useState } from "react";
import ManageUsers from "../components/ManageUsers";
import Top from "../components/Top";
import AddAdmin from "../components/AddAdmin";
import AdminDashBoard from "../components/AdminDashBoard";


const Admin = () => {
  const [activeTab, setActiveTab] = useState("manageUsers");

  const handleTabChange = (tab) => {
    setActiveTab(tab);
  };

  const renderTabContent = () => {
    switch (activeTab) {
      case "manageUsers":
        return <ManageUsers />;
      case "top":
        return <Top />;
      case "addadmin":
        return<AddAdmin/>;
      case "admindashboard":
        return <AdminDashBoard/>;
    
      default:
        return null;
    }
  };

  return (
    <div className="text-black flex w-[96%]">
      <div className="relative flex flex-col bg-clip-border rounded-xl bg-white text-gray-700 h-[calc(100vh-2rem)] w-full max-w-[20rem] p-4 shadow-xl shadow-blue-gray-900/5">
        <div className="mb-2 p-4">
          <h5 className="block antialiased tracking-normal font-sans text-xl font-semibold leading-snug text-gray-900">
            Admin
          </h5>
        </div>
        <nav className="flex flex-col gap-1 min-w-[240px] p-2 font-sans text-base font-normal text-gray-700">
        <div
            role="button"
            tabIndex={0}
            className={`flex items-center w-full p-3 rounded-lg text-start leading-tight transition-all ${
              activeTab === "admindashboard"
                ? "bg-blue-50 bg-opacity-80 text-blue-900"
                : "hover:bg-blue-50 hover:bg-opacity-80 focus:bg-blue-50 focus:bg-opacity-80 active:bg-gray-50 active:bg-opacity-80 hover:text-blue-900 focus:text-blue-900 active:text-blue-900"
            } outline-none`}
            onClick={() => handleTabChange("admindashboard")}
          >
            Admin DashBoard
          </div>
          <div
            role="button"
            tabIndex={0}
            className={`flex items-center w-full p-3 rounded-lg text-start leading-tight transition-all ${
              activeTab === "manageUsers"
                ? "bg-blue-50 bg-opacity-80 text-blue-900"
                : "hover:bg-blue-50 hover:bg-opacity-80 focus:bg-blue-50 focus:bg-opacity-80 active:bg-gray-50 active:bg-opacity-80 hover:text-blue-900 focus:text-blue-900 active:text-blue-900"
            } outline-none`}
            onClick={() => handleTabChange("manageUsers")}
          >
            Manage Users
          </div>
          <div
            role="button"
            tabIndex={0}
            className={`flex items-center w-full p-3 rounded-lg text-start leading-tight transition-all ${
              activeTab === "top"
                ? "bg-blue-50 bg-opacity-80 text-blue-900"
                : "hover:bg-blue-50 hover:bg-opacity-80 focus:bg-blue-50 focus:bg-opacity-80 active:bg-gray-50 active:bg-opacity-80 hover:text-blue-900 focus:text-blue-900 active:text-blue-900"
            } outline-none`}
            onClick={() => handleTabChange("top")}
          >
            Top 
          </div>
          <div
            role="button"
            tabIndex={0}
            className={`flex items-center w-full p-3 rounded-lg text-start leading-tight transition-all ${
              activeTab === "addadmin"
                ? "bg-blue-50 bg-opacity-80 text-blue-900"
                : "hover:bg-blue-50 hover:bg-opacity-80 focus:bg-blue-50 focus:bg-opacity-80 active:bg-gray-50 active:bg-opacity-80 hover:text-blue-900 focus:text-blue-900 active:text-blue-900"
            } outline-none`}
            onClick={() => handleTabChange("addadmin")}
          >
            Add Admin
          </div>
          
          
        </nav>
      </div>
      <div className="p-10 w-full">
      {renderTabContent()}
      

      </div>
      
    </div>
  );
};


export default Admin;