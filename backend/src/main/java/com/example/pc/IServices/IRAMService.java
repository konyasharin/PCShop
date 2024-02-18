package com.example.pc.IServices;

import com.example.pc.Models.RAMModel;

import java.util.List;

public interface IRAMService {

    List<RAMModel> getRAM();
    RAMModel getRAMById(Long id);
    RAMModel addRAM(RAMModel ramModel);
    void deleteRAMById(Long id);
}
