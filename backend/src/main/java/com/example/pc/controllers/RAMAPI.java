package com.example.pc.controllers;

import com.example.pc.Models.ComputerCaseModel;
import com.example.pc.Models.RAMModel;
import com.example.pc.Services.RAMService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/api")
public class RAMAPI {

    private static final Logger logger = LoggerFactory.getLogger(RAMAPI.class);

    @Autowired
    private RAMService ramService;

    @PostMapping("/createComponent/RAM")
    public ResponseEntity<Map<String, Object>> createRAM(@RequestParam("brand") String brand,
                                                                  @RequestParam("model") String model,
                                                                  @RequestParam("description") String description,
                                                                  @RequestParam("price") Integer price,
                                                                  @RequestParam("country") String country,
                                                                  @RequestParam("frequency") Integer frequency,
                                                                  @RequestParam("timings") Integer timings,
                                                                  @RequestParam("capacity_db") Integer capacity_db,
                                                                  @RequestParam("image") MultipartFile image){

        try{

            RAMModel ramModel = new RAMModel();
            ramModel.setBrand(brand);
            ramModel.setModel(model);
            ramModel.setDescription(description);
            ramModel.setPrice(price);
            ramModel.setCountry(country);
            ramModel.setFrequency(frequency);
            ramModel.setTimings(timings);
            ramModel.setCapacity_gb(capacity_db);
            ramModel.setImage(image.getBytes());

            Map<String, Object> RamResponse = new HashMap<>();
            RamResponse.put("brand", brand);
            RamResponse.put("model", model);
            RamResponse.put("description", description);
            RamResponse.put("price", price);
            RamResponse.put("country", country);
            RamResponse.put("frequency", frequency);
            RamResponse.put("timings", timings);
            RamResponse.put("capacity_db", capacity_db);
            RamResponse.put("image", image.getBytes());

            logger.info("Ram created successfully: {}", RamResponse);

            return new ResponseEntity<>(RamResponse, HttpStatus.CREATED);

        }catch(IOException e){
            e.printStackTrace();

            logger.error("Failed to create Ram", e);

            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }

    }

}
