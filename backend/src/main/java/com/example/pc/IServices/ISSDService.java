package com.example.pc.IServices;

import com.example.pc.Models.SSDModel;
import com.example.pc.dto.SsdDto;

import java.util.List;

public interface ISSDService {

    List<SSDModel> getSSD();
    SSDModel getSSDById(Long id);
    SSDModel updateSSD(SSDModel ssdModel);
    void deleteSSDById(Long id);
    SSDModel createSSD(SsdDto ssdDto);
}
