package com.example.pc.IServices;

import com.example.pc.Models.RAMModel;
import com.example.pc.dto.RamDto;

import java.util.List;

public interface IRAMService {

    List<RAMModel> getRAM();
    RAMModel getRAMById(Long id);
    RAMModel updateRAM(RAMModel ramModel);
    void deleteRAMById(Long id);
    RAMModel createRAM(RamDto ramDto);
}
